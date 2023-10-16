using Autofac;
using Autofac.Extras.Moq;
using Core.Context;
using Core.Dependency;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Autofac.Core;
using Core.MultiTenancy;
using Microsoft.Extensions.Configuration;

namespace Core.Application;

public abstract class BaseTestService
{
    private AutoMock _autoMock;

    public BaseTestService()
    {
        _autoMock = AutoMock.GetLoose(ServiceRegistrar);

    }

    private void ServiceRegistrar(ContainerBuilder cb)
    {
        var moduleType = AssemblyRegistrar.GetListTypeModule();

        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        // Build config
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../../../Api"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetSection("TenantOptions")["DefaultConnection"];

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(connectionString)
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

    protected AutoMock AutoMock => _autoMock;
}