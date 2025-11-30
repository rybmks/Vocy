using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class VocyDbContext(DbContextOptions<VocyDbContext> options) : DbContext(options)
{
    public DbSet<Domain.User.User> Users => Set<Domain.User.User>();
}