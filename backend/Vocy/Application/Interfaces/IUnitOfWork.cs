using Domain.Base;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    public IRepository<T> GetRepository<T>() where T : BaseIdentity;
}