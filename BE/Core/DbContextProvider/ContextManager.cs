using Core.DI;
using Microsoft.EntityFrameworkCore;

namespace Core.DbContextProvider;

public interface IContextManager
{
    DbContext CurrentContext();
}

public class ContextManager: IContextManager, ITransientDependency
{
    private readonly DbContext _dbContext;
    
    public ContextManager(IDbContextProvider<SqLiteContext> dbContext)
    {
        _dbContext = dbContext.GetDbContext();
    }

    public DbContext CurrentContext()
    {
        return _dbContext;
    }
}