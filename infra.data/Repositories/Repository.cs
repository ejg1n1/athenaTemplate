using Infrastructure.Data;

namespace Athena.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> Query(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> QueryAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
        
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

}
