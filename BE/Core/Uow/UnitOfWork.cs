using Core.Context;
using Core.Dependency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Uow
{
    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction();
        Task SaveChangeAsync();
    }
    
    public class UnitOfWork<TContext> : IUnitOfWork, ITransientDependency, IDisposable
        where TContext : DbContext
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(IDbContextProvider<TContext> dbContextProvider)
        {
            _dbContext = dbContextProvider.GetDbContext();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if(disposing)
                    _dbContext.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
