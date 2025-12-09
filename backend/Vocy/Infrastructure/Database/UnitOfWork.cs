using Application.Interfaces;
using Domain.Base;

namespace Infrastructure.Database;

public class UnitOfWork(VocyDbContext context) : IUnitOfWork
{
    public IRepository<T> GetRepository<T>() where T : BaseIdentity
    {
        return new PgGenericRepository<T>(context);
    }


    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}