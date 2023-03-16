namespace Educative.Infrastructure.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIDAsync(string? ID);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string ID);
    }
}
