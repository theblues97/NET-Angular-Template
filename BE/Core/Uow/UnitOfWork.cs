using Core.DbContextProvider;
using Core.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Uow
{
    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction();
        Task SaveChangeAsync();
    }
    
    public class UnitOfWork : IUnitOfWork, ITransientDependency, IDisposable
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(IContextManager contextManager)
        {
            _dbContext = contextManager.CurrentContext();
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
