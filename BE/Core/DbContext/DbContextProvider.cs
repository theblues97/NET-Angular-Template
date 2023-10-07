using Core.Dependency;
using Microsoft.EntityFrameworkCore;

namespace Core.Context;

public interface IDbContextProvider<TContext> where TContext : DbContext
{
    TContext GetDbContext();
}

public class DbContextProvider<TContext> : IDbContextProvider<TContext>, ITransientDependency
    where TContext : DbContext
{
    private readonly TContext _dbContext;
    public DbContextProvider(TContext context)
    {
        _dbContext = context;
    }

    public TContext GetDbContext()
    {
        return _dbContext;
    }
}