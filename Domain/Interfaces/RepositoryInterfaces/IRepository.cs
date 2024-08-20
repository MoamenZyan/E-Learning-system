namespace E_Learning_Platform_API.Domain.Interfaces.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        Task<bool> AddAsync(T entity);
        bool UpdateAsync(T entity);
        bool DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<bool> SaveChanges();
        IEnumerable<T>? Filter(Func<T, bool> filter);
    }
}
