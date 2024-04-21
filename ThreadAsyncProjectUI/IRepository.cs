namespace ThreadAsyncProjectUI;

public interface IRepository<T>
{
    Task<IQueryable<T>> GetAllAsync(CancellationToken token);
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T item);
}
