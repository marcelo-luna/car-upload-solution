namespace FineData.Persistence.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddBulkAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task Delete(T entity);
    }
}
