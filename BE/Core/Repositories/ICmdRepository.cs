namespace Core.Repositories
{
    public interface ICmdRepository<TEntity> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false);
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false);
        Task DeleteAsync(TEntity entity, bool autoSave = false);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false);
    }
}
