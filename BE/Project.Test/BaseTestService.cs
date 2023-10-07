using Autofac;
using Autofac.Extras.Moq;
using Core.Context;
using Core.Dependency;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;

namespace Core.Application;

public abstract class BaseTestService
{

    public BaseTestService()
    {
    }

    private void ServiceRegistrar(ContainerBuilder cb)
    {
        var moduleType = AssemblyRegistrar.GetListTypeModule();

        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        if (baseDir.Contains("bin"))
        {
            int index = baseDir.IndexOf("bin");
            baseDir = baseDir.Substring(0, index);
            baseDir = Directory.GetParent(baseDir)?.Parent?.FullName ?? "";
        }
        var DbPath = $"Data Source={Path.Join(baseDir, "product.db")}";

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(DbPath)
            .Options;

        cb.RegisterType<AppDbContext>()
            .WithParameter("options", options)
            .InstancePerLifetimeScope();

        cb.RegisterGeneric(typeof(DbContextProvider<>))
            .As(typeof(IDbContextProvider<>))
            .InstancePerDependency(); 

        moduleType.ForEach(module =>
        {
            cb.GenericRepositoryRegistrar(module);
            var assignableToBaseService = module.Assembly
            .GetTypes()
            .Where(type => !type.IsAbstract && typeof(BaseService).IsAssignableFrom(type));

            foreach (var service in assignableToBaseService)
            {
                cb.RegisterType(service).PropertiesAutowired();
            }
        });
    }

    protected AutoMock AutoMock => AutoMock.GetLoose(ServiceRegistrar);
}