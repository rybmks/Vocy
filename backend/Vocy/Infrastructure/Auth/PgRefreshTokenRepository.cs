using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Auth;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Auth;

public class PgRefreshTokenRepository(VocyDbContext ctx) : IRepository<RefreshToken>
{
    private readonly VocyDbContext _ctx = ctx;

    #region Create

    public async Task<Guid> Save(RefreshToken token)
    {
        await _ctx.RefreshTokens.AddAsync(token);
        await _ctx.SaveChangesAsync();
        return token.Id;
    }

    #endregion

    #region Get

    public async Task<RefreshToken> Get(Guid id) =>
        await _ctx.RefreshTokens.FindAsync(id) ?? throw new InvalidOperationException();

    public async Task<RefreshToken> GetFirstOrDefault(Expression<Func<RefreshToken, bool>> expr) =>
        await _ctx.RefreshTokens.FirstOrDefaultAsync(expr) ?? throw new InvalidOperationException();

    public async Task<IEnumerable<RefreshToken>> GetAll(Expression<Func<RefreshToken, bool>> expr) =>
        await _ctx.RefreshTokens.Where(expr).ToListAsync();

    #endregion

    #region Delete

    public async Task Remove(Guid id)
    {
        var token = await _ctx.RefreshTokens.FindAsync(id);
        if (token != null)
        {
            _ctx.RefreshTokens.Remove(token);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task Remove(RefreshToken entity)
    {
        _ctx.RefreshTokens.Remove(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task RemoveAll(Expression<Func<RefreshToken, bool>> expr)
    {
        var tokens = _ctx.RefreshTokens.Where(expr);
        _ctx.RefreshTokens.RemoveRange(tokens);
        await _ctx.SaveChangesAsync();
    }

    #endregion

    #region Update

    public async Task Update(RefreshToken entity)
    {
        _ctx.RefreshTokens.Update(entity);
        await _ctx.SaveChangesAsync();
    }

    #endregion
}