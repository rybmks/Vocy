using Domain.Auth;
using Infrastructure.TypeConfigurations;

namespace Infrastructure.Database;

using Domain.User;
using Microsoft.EntityFrameworkCore;

public class VocyDbContext(DbContextOptions<VocyDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Enum configuration
        modelBuilder.HasPostgresEnum<ConnectionRole>();

        // Domain type configuration    
        new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
        new ConnectionEntityTypeConfiguration().Configure(modelBuilder.Entity<Connection>());
    }

    public DbSet<User> Users { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Connection> Connections { get; set; }
}