using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IQueryRepository<T> where T : class
    {
        T? Find(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
        Task<IEnumerable<T>> GetListAsync();
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<int> GetCountAsync();
    }
}
