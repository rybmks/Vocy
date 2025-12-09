using Domain.Auth;
using Infrastructure.User;

namespace Infrastructure.Database;

using Domain.User;
using Microsoft.EntityFrameworkCore;

public class VocyDbContext(DbContextOptions<VocyDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
    }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    // public DbSet<Connection> Connections { get; set; }
}