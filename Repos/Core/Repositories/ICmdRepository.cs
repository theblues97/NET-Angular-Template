namespace Core.Repositories
{
    public interface ICmdRepository<T> where T : class
    {
        Task<T> InsertAsync(T entity, bool autoSave = false);
        Task InsertRangeAsync(IEnumerable<T> entities, bool autoSave = false);
        Task<T> UpdateAsync(T entity, bool autoSave = false);
        Task UpdateRangeAsync(IEnumerable<T> entities, bool autoSave = false);
        Task DeleteAsync(T entity, bool autoSave = false);
        Task DeleteRangeAsync(IEnumerable<T> entities, bool autoSave = false);
    }
}
