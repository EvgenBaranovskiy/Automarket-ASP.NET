
namespace Automarket.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<int> CreateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<List<T>> SelectAsync();
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
