using Domain.Base;

namespace Application.User;

public interface IRepository<T> where T : BaseIdentity
{
    Task Save(T entity);
}