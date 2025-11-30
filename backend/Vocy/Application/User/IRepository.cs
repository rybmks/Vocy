namespace Application.User;

public interface IRepository<T> where T : BaseIdentity
{
    Task<T> Get(Guid Id);
    Task Save(T entity);
    Task Remove(T entity);
    Task Remove(Guid id);
}
