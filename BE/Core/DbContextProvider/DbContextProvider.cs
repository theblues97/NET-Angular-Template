using Core.DI;
using Microsoft.EntityFrameworkCore;

namespace Core.DbContextProvider;

public interface IDbContextProvider<TContext> where TContext : DbContext
{
    TContext GetDbContext();
}

public class DbContextProvider<TContext> : IDbContextProvider<TContext>, ITransientDependency where TContext: DbContext
{
    private readonly TContext _dbContext;
    //public DbContextProvider(IServiceProvider serviceProvider)
    //{
    //    _dbContext = (TContext)serviceProvider.GetService(typeof(TContext))!;
    //}
    public DbContextProvider(TContext context)
    {
        _dbContext = context;
    }

    public TContext GetDbContext()
    {
        return _dbContext;
    }
}