using Application.Production;
using Autofac;
using Autofac.Extras.Moq;
using Core.Application;
using Core.Dependency;
using Core.Utilities.Dependency;

namespace Project.Test;

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
        moduleType.ForEach(module =>
        {
            var assignableToBaseService = module.Assembly
            .GetTypes()
            .Where(type => type.IsAssignableToGenericType(typeof(BaseService)));

            foreach (var service in assignableToBaseService)
            {
                cb.RegisterType(service).PropertiesAutowired();
            }
        });
    }

    protected AutoMock AutoMock => _autoMock;
}