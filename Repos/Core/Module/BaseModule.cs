using Autofac;
using Autofac.Builder;
using Core.DI;
using System.Reflection;

namespace Core.Module;

public abstract class BaseModule : Autofac.Module
{
    //protected override void Load(ContainerBuilder builder)
    //{
    //    // Subscribe to the diagnostics with your tracer.
    //    //var assemblies = AppDomain.CurrentDomain.GetAssemblies();

    //    //Generic
    //    //builder.RegisterAssemblyOpenGenericTypes(assembly)
    //    //    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ITransientDependency))))
    //    //    .AsImplementedInterfaces()
    //    //    .InstancePerDependency();
        
    //    //builder.RegisterAssemblyOpenGenericTypes(assembly)
    //    //    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IScopedDependency))))
    //    //    .AsImplementedInterfaces()
    //    //    .InstancePerDependency();
        
    //    //builder.RegisterAssemblyOpenGenericTypes(assembly)
    //    //    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ISingletonDependency))))
    //    //    .AsImplementedInterfaces()
    //    //    .InstancePerDependency();
        
    //    ////Normal    
    //    //builder
    //    //    .RegisterAssemblyTypes(assembly)
    //    //    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ITransientDependency))))
    //    //    .PropertiesAutowired((pros, ins)
    //    //        => pros.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPropertyInjection))))
    //    //    .AsImplementedInterfaces()
    //    //    .InstancePerDependency();
        
    //    //builder
    //    //    .RegisterAssemblyTypes(assembly)
    //    //    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IScopedDependency))))
    //    //    .PropertiesAutowired((pros, ins)
    //    //        => pros.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPropertyInjection))))
    //    //    .AsImplementedInterfaces()
    //    //    .InstancePerLifetimeScope();
        
    //    //builder
    //    //    .RegisterAssemblyTypes(assembly)
    //    //    .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(ISingletonDependency))))
    //    //    .PropertiesAutowired((pros, ins)
    //    //        => pros.PropertyType.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IPropertyInjection))))
    //    //    .AsImplementedInterfaces()
    //    //    .SingleInstance();

    //    //base.Load(builder);
    //}
}