using Infrastructure.Database;

namespace Infrastructure.User;

using Application.User;
using Domain.User;
using Microsoft.EntityFrameworkCore;

public class PgUserRepository(VocyDbContext ctx) : IUserRepository
{
    private readonly VocyDbContext _ctx = ctx;

    public async Task SaveUser(User user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task RemoveUser(User user)
    {
        _ctx.Users.Remove(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task<User> GetUser(Guid Id) => await _ctx.Users.FindAsync(Id)
        ?? throw new Exception();

    public Task RemoveUser(Guid id)
    {
        throw new NotImplementedException();
    }
}

public class PgUserRepositoryExeception : Exception
{
    public PgUserRepositoryExeception(string message) : base(message)
    {
    }
}