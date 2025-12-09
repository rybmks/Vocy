using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class PgGenericRepository<T>(VocyDbContext context) : IRepository<T> where T : BaseIdentity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T> GetAsync(Guid id) =>
        await _dbSet.FindAsync(id) ?? throw new InvalidOperationException();


    public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> expr) =>
        await _dbSet.FirstOrDefaultAsync(expr) ?? throw new InvalidOperationException();

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expr) =>
        await _dbSet.Where(expr).ToListAsync();


    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task RemoveAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            throw new InvalidOperationException();

        _dbSet.Remove(entity);
    }

    public virtual void RemoveAll(Expression<Func<T, bool>> expr)
    {
        var entities = _dbSet.Where(expr);
        _dbSet.RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual async Task<Guid> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity.Id;
    }
}