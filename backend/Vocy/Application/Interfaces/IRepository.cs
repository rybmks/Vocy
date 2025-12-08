using System.Linq.Expressions;
using Domain.Base;

namespace Application.Interfaces;

public interface IRepository<T> where T : BaseIdentity
{
    // TODO: Change giud to BaseIdentity

    //Get
    Task<T> Get(Guid id);
    Task<T> GetFirstOrDefault(Expression<Func<T, bool>> expr);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expr);

    //Remove
    Task Remove(T entity);
    Task Remove(Guid id);
    Task RemoveAll(Expression<Func<T, bool>> expr);

    //Update
    Task Update(T entity);

    //Save
    Task<Guid> Save(T entity);
}