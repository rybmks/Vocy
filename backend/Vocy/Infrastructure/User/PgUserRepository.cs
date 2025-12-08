using System.Linq.Expressions;
using Application.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.User;

using Domain.User;

public class PgUserRepository(VocyDbContext ctx) : IRepository<User>
{
    private readonly VocyDbContext _ctx = ctx;

    #region Create

    public async Task<Guid> Save(User entity)
    {
        await _ctx.Users.AddAsync(entity);
        await _ctx.SaveChangesAsync();
        return entity.Id;
    }

    #endregion

    #region Get

    public async Task<User> Get(Guid id) => await _ctx.Users.FindAsync(id) ?? throw new InvalidOperationException();

    public async Task<User> GetFirstOrDefault(Expression<Func<User, bool>> expr) =>
        await _ctx.Users.FirstOrDefaultAsync(expr) ?? throw new InvalidOperationException();

    public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> expr) =>
        await _ctx.Users.Where(expr).ToListAsync();

    #endregion

    #region Delete

    public async Task Remove(Guid id)
    {
        var user = await _ctx.Users.FindAsync(id);
        if (user != null)
        {
            _ctx.Users.Remove(user);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task Remove(User entity)
    {
        _ctx.Users.Remove(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task RemoveAll(Expression<Func<User, bool>> expr)
    {
        var users = _ctx.Users.Where(expr);
        _ctx.Users.RemoveRange(users);
        await _ctx.SaveChangesAsync();
    }

    #endregion

    #region Update

    public async Task Update(User entity)
    {
        _ctx.Users.Update(entity);
        await _ctx.SaveChangesAsync();
    }

    #endregion
}