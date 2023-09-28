using System.Linq.Expressions;
using Core.DbContextProvider;
using Core.DI;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public interface IRepository<T> : IQueryRepository<T>, ICmdRepository<T> where T : class
    {
    }
    
    public class Repository<T> : IRepository<T>, IScopedDependency where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(IContextManager contextManager)
        {
            _dbContext = contextManager.CurrentContext();
            _dbSet = _dbContext.Set<T>();
        }
        public T? Find(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return await _dbSet.Where(predicate).SingleOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public Task<IEnumerable<T>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet;
        }
        
        public async Task<T> InsertAsync(T entity, bool autoSave = false)
        {
            var rs = _dbSet.Add(entity).Entity;

            if (autoSave)
                await _dbContext.SaveChangesAsync();

            return rs;
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities, bool autoSave = false)
        {
            await _dbSet.AddRangeAsync(entities);

            if(autoSave)
                await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity, bool autoSave = false)
        {
            var rs = _dbSet.Update(entity).Entity;

            if (autoSave)
                await _dbContext.SaveChangesAsync();

            return rs;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities, bool autoSave = false)
        {
            _dbSet.UpdateRange(entities);

            if (autoSave)
                await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(T entity, bool autoSave = false)
        {
            _dbSet.Remove(entity);

            if (autoSave)
                await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool autoSave = false)
        {
            _dbSet.RemoveRange(entities);

            if (autoSave)
                await _dbContext.SaveChangesAsync();
        }
    }
}
