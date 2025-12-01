namespace Infrastructure.Database;

using Domain.User;
using Microsoft.EntityFrameworkCore;

public class VocyDbContext(DbContextOptions<VocyDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}