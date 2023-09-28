using System.Net.NetworkInformation;
using Core.DI;
using System.Reflection;
using Autofac;
using Autofac.Core;
using PostSharp.Aspects.Advices;
using Autofac.Diagnostics;

namespace Api.DI
{
    public static class AssemblyRegistrar
    {
        public static void AutofacRegister(this ContainerBuilder builder)
        {
            const string assembliesFetchPattern = "*.dll";
        
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(path))
                return;

            var assemblies = Directory
                .GetFiles(path, assembliesFetchPattern, SearchOption.TopDirectoryOnly)
                .Select(Assembly.LoadFrom)
                .ToList();

            assemblies
            .ForEach(assembly =>
            {
                assembly.GetTypes()
                    .Where(p => typeof(IModule).IsAssignableFrom(p) && !p.IsAbstract)
                    .ToList()
                    .ForEach(t =>
                    {
                        builder.LoadModule(t.Assembly);
                        builder.RegisterModule((IModule)Activator.CreateInstance(t)!);
                        builder.AutoFacLogging();
                    });
            });
        }

        private static void LoadModule(this ContainerBuilder builder, Assembly assembly)
        {
            //Generic
            builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ITransientDependency))))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IScopedDependency))))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ISingletonDependency))))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //Normal    
            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ITransientDependency))))
                .PropertiesAutowired((pros, ins)
                    => pros.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPropertyInjection))))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IScopedDependency))))
                .PropertiesAutowired((pros, ins)
                    => pros.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPropertyInjection))))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ISingletonDependency))))
                .PropertiesAutowired((pros, ins)
                    => pros.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPropertyInjection))))
                .AsImplementedInterfaces()
                .SingleInstance();
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
