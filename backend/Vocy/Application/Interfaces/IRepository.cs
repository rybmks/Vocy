using Domain.Base;

namespace Application.User;

public interface IRepository<T> where T : BaseIdentity
{
    // TODO: Change giud to BaseIdentity
    Task<T> Get(Guid id);
    Task Remove(Guid id);
    Task Remove(T entity);
    Task Save(T entity);
}