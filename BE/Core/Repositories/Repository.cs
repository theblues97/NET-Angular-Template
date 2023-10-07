using System.Linq.Expressions;
using Core.Context;
using Core.Context.Entity;
using Core.Dependency;
using Core.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public interface IRepository<TEntity> : IQueryRepository<TEntity>, ICmdRepository<TEntity> 
        where TEntity : BaseEntity
    {
    }
    
    public class Repository<TEntity, TContext> : IRepository<TEntity>, ITransientDependency
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(IDbContextProvider<TContext> dbContextProvider)
        {
            _dbContext = dbContextProvider.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
        }
        public TEntity? Find(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
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

        public Task<IEnumerable<TEntity>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet;
        }
        
        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false)
        {
            //entity.TenantId = _tenant.TenantId;
            var rs = _dbSet.Add(entity).Entity;

            if (autoSave)
                await _dbContext.SaveChangesAsync();

            return rs;
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            //foreach (var entry in entities)
            //{
            //    entry.TenantId = _tenant.TenantId;
            //}
            await _dbSet.AddRangeAsync(entities);

            if(autoSave)
                await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false)
        {
            var rs = _dbSet.Update(entity).Entity;

            if (autoSave)
                await _dbContext.SaveChangesAsync();

            return rs;
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            _dbSet.UpdateRange(entities);

            if (autoSave)
                await _dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(TEntity entity, bool autoSave = false)
        {
            _dbSet.Remove(entity);

            if (autoSave)
                await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            _dbSet.RemoveRange(entities);

            if (autoSave)
                await _dbContext.SaveChangesAsync();
        }
    }
}
