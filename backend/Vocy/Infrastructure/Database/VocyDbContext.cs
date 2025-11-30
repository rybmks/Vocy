namespace Infrastructure.Database;

using Domain.User;
using Microsoft.EntityFrameworkCore;

public class VocyDbContext : DbContext
{
    public VocyDbContext(DbContextOptions<VocyDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}