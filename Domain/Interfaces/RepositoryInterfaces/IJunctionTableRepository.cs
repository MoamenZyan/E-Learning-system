namespace E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces
{
    public interface IJunctionTableRepository<T>
    {
        Task<bool> AddAsync(T entity);
        bool UpdateAsync(T entity);
        bool DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id1, int id2);
        Task<IEnumerable<T>?> GetAllAsync(int id);
        Task<bool> SaveChanges();
    }
}
