using Autofac;
using Core.Dependency;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Core.Utilities.Dependency
{
    public static class DependencyHelper
    {
        public static void AutofacGenericRegisterBuilder<TDependencyLifeTime>(this ContainerBuilder builder, Assembly assembly)
            where TDependencyLifeTime : class
        {
            var implement = builder.RegisterAssemblyOpenGenericTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(TDependencyLifeTime))))
                .AsImplementedInterfaces();

            switch (typeof(TDependencyLifeTime))
            {
                case ITransientDependency:
                    implement.InstancePerDependency();
                    break;
                case IScopedDependency:
                    implement.InstancePerLifetimeScope();
                    break;
                case ISingletonDependency:
                    implement.SingleInstance();
                    break;
                default:
                    implement.InstancePerDependency();
                    break;
            }
        }

        public static void AutofacRegisterBuilder<TDependencyLifeTime>(this ContainerBuilder builder, Assembly assembly)
            where TDependencyLifeTime : class
        {

			var implement = builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(TDependencyLifeTime))))
                .PropertiesAutowired((propertyInfo, instance)
                    => propertyInfo.GetCustomAttribute<PropertyDependence>() != null)
                .AsImplementedInterfaces();

            switch (typeof(TDependencyLifeTime))
            {
                case ITransientDependency:
                    implement.InstancePerDependency();
                    break;
                case IScopedDependency:
                    implement.InstancePerLifetimeScope();
                    break;
                case ISingletonDependency:
                    implement.SingleInstance();
                    break;
                default:
                    implement.InstancePerDependency();
                    break;
            }
        }

        public static void AutofacTypeRegisterBuilder<TDependencyLifeTime>(this ContainerBuilder builder, Type type)
			where TDependencyLifeTime : class
		{
            var implement = builder.RegisterType(type)
                .AsImplementedInterfaces();

			switch (typeof(TDependencyLifeTime))
			{
				case ITransientDependency:
					implement.InstancePerDependency();
					break;
				case IScopedDependency:
					implement.InstancePerLifetimeScope();
					break;
				case ISingletonDependency:
					implement.SingleInstance();
					break;
				default:
					implement.InstancePerDependency();
					break;
			}
		}

        public static IEnumerable<EntityTypeInfo> GetEntityTypeInfos(this Type dbContextType)
        {
            return from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   where IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))
                   select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);

        }

        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            //if (!givenType.IsGenericType)
            //    return false;

            //var genericGivenType = givenType.GetGenericTypeDefinition();
            //return genericGivenType.IsGenericType && genericGivenType.GetGenericTypeDefinition() == genericType;
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        public class EntityTypeInfo
        {
            public Type EntityType { get; private set; }
            public Type? DeclaringType { get; private set; }

            public EntityTypeInfo(Type entityType, Type? declaringType)
            {
                EntityType = entityType;
                DeclaringType = declaringType;
            }

        }
    }
}
