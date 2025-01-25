
namespace Infrastructure.Domain
{
    public interface IGenericRepository<T, TId> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TId id);
        Task<IEnumerable<T>> GetPaginatedDataAsync(int pageNumber, int count);
    }
}
