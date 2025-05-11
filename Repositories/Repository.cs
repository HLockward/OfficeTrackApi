using Microsoft.EntityFrameworkCore;

namespace OfficeTrackApi.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApiDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApiDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
        {
            query = include(query);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
        {
            query = include(query);
        }

        var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id)
        ??
        throw new InvalidOperationException($"Entity of type {typeof(T).Name} with Id {id} was not found.");
        return entity;
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
