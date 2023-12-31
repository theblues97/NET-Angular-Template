﻿using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Diagnostics;
using Core.Utilities.Dependency;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using static Core.Utilities.Dependency.DependencyHelper;
using Core.Uow;

namespace Core.Dependency
{
    public static class AssemblyRegistrar
    {
        public static void AutofacRegister(this ContainerBuilder builder)
        {
            var listTypeModule = GetListTypeModule();

            listTypeModule.ForEach(type =>
            {
                builder.GenericRepositoryRegistrar(type);
                builder.ModuleRegistrar(type);
                builder.RegisterModule((IModule)Activator.CreateInstance(type)!);
            });     
            //builder.AutoFacLogging();
        }

        public static List<Type>? GetListTypeModule()
        {
            List<Type> moduleTypes = new();
            const string assembliesFetchPattern = "*.dll";

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(path))
                return null;

            var applicationAsseblies = Directory
                .GetFiles(path, assembliesFetchPattern, SearchOption.TopDirectoryOnly)
                .Select(Assembly.LoadFrom)
                .ToList();
            foreach (var item in applicationAsseblies)
            {
                var moduleType = item?.GetTypes()
                    .Where(p => typeof(IModule).IsAssignableFrom(p) && !p.IsAbstract)
                    .ToList();
                moduleTypes.AddRange(moduleType);
            }
            return moduleTypes;
        }

        public static void GenericRepositoryRegistrar(this ContainerBuilder builder, Type moduleType)
        {
            var dbContextTypes = moduleType.Assembly
                .GetTypes()
                .Where(type => type.IsPublic && !type.IsAbstract &&
                        type.IsClass &&
                        typeof(DbContext).IsAssignableFrom(type));

            foreach (var dbtType in dbContextTypes)
            {
                var entityTypeInfos = dbtType.GetEntityTypeInfos();
                foreach (var entityTypeInfo in entityTypeInfos)
                {
                    var repositoryType = typeof(Repository<,>).MakeGenericType(entityTypeInfo.EntityType, entityTypeInfo.DeclaringType);
                    builder.AutofacTypeRegisterBuilder<IScopedDependency>(repositoryType);
				}

                var uowType = typeof(UnitOfWork<>).MakeGenericType(dbtType);
                builder.AutofacTypeRegisterBuilder<IScopedDependency>(uowType);
            } 
        }

        private static void ModuleRegistrar(this ContainerBuilder builder, Type moduleType)
        {
            var depedents = moduleType.Assembly
                .GetTypes()
                .ToList();

			builder.ModuleDependencyRegister(moduleType.Assembly);
        }

        private static void ModuleDependencyRegister(this ContainerBuilder builder, Assembly assembly)
        {
            ////Generic
            builder.AutofacGenericRegisterBuilder<ITransientDependency>(assembly);
            builder.AutofacGenericRegisterBuilder<IScopedDependency>(assembly);
            builder.AutofacGenericRegisterBuilder<ISingletonDependency>(assembly);

            //Normal
            builder.AutofacRegisterBuilder<ITransientDependency>(assembly);
            builder.AutofacRegisterBuilder<IScopedDependency>(assembly);
            builder.AutofacRegisterBuilder<ISingletonDependency>(assembly);
        }

        private static void AutoFacLogging(this ContainerBuilder builder)
        {
            var tracer = new DefaultDiagnosticTracer();
            tracer.OperationCompleted += (sender, args) =>
            {
                Console.WriteLine(args.TraceContent);
            };

            builder.RegisterBuildCallback(c =>
            {
                var container = c as IContainer;
                container.SubscribeToDiagnostics(tracer);
            });
        }
    } 
}
