using Infrastructure.Database;

namespace Infrastructure.User;

using Application.User;
using Domain.User;

public class PgUserRepository(VocyDbContext ctx) : IUserRepository
{
    private readonly VocyDbContext _ctx = ctx;

    public async Task CreateUser(Domain.User.User user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
    }

    public Task RemoveUser(Guid userId)
    {
        throw new NotImplementedException();
    }
}