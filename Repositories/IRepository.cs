using System.Linq.Expressions;

namespace OfficeTrackApi.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
    public Task<T?> FindSingleAsync(Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IQueryable<T>>? include = null);
    void Update(T entity);
    void Delete(T entity);

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}
