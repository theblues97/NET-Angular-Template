using Core.DI;
using Core.Uow;

namespace Core.Application;

public abstract class BaseService: ITransientDependency
{
    public IUnitOfWork UnitOfWork { get; set; }

    //public BaseService(IUnitOfWork unitOfWork)
    //{
    //    UnitOfWork = unitOfWork;
    //}
}