using Autofac;
using Autofac.Extras.Moq;
using Core.Context.Entity;
using Core.Repositories;
using Moq;

namespace Project.Test;

public abstract class BaseServiceTest
{
    private AutoMock _autoMock;

    public BaseServiceTest()
    {
        _autoMock = AutoMock.GetLoose(ServiceRegistrar);
    }

    /// <summary>
    /// Registing services inherit from BaseService
    /// </summary>
    /// <param name="cb"></param>
    protected abstract void ServiceRegistrar(ContainerBuilder cb);

    protected AutoMock AutoMock => _autoMock;
}

public static class MockRepositories
{
    public static void MockRepository<T>(this AutoMock autoMock) where T : BaseEntity
    {
        autoMock.Mock<ICmdRepository<T>>()
                .Setup(x => x.InsertAsync(It.IsAny<T>(), false)).ReturnsAsync(Mock.Of<T>());
        autoMock.Mock<IQueryRepository<T>>()
            .Setup(x => x.GetCountAsync()).ReturnsAsync(1);
        autoMock.Mock<IRepository<T>>()
            .Setup(x => x.InsertAsync(It.IsAny<T>(), false)).ReturnsAsync(Mock.Of<T>());
    }
}