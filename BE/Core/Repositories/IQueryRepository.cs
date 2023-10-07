using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        TEntity? Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetQueryable();
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> GetCountAsync();
    }
}
