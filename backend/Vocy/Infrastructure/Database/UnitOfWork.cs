using Application.Interfaces;
using Domain.Base;

namespace Infrastructure.Database;

public class UnitOfWork(VocyDbContext context) : IUnitOfWork
{
    private readonly Dictionary<Type, object> _repositories = new();

    public IRepository<T> GetRepository<T>() where T : BaseIdentity
    {
        var type = typeof(T);
        if (_repositories.TryGetValue(type, out var repository)) return (IRepository<T>)repository;

        repository = new PgGenericRepository<T>(context);
        _repositories[type] = repository;


        return (IRepository<T>)repository;
    }


    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}