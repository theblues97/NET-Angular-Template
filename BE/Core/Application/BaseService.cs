using Core.Dependency;
using Core.MultiTenancy;
using Core.Repositories;
using Core.Uow;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public abstract class BaseService : ITransientDependency
{
    [PropertyDependence]
    public IServiceProvider ServiceProvider { get; set; } = null!;
    public IUnitOfWork UnitOfWork => ServiceProvider.GetRequiredService<IUnitOfWork>();
}