using Core.Dependency;
using Core.MultiTenancy;
using Core.Uow;

namespace Core.Application;

public abstract class BaseService : ITransientDependency
{
    public IUnitOfWork UnitOfWork { get; set; }

    //public ICurrentTenant CurrentTenant { get; set; }
}