using System.Linq.Expressions;
using Domain.Base;

namespace Application.Interfaces;

public interface IRepository<T> where T : BaseIdentity
{
    // TODO: Change giud to BaseIdentity

    //Get
    Task<T> GetAsync(Guid id);
    Task<T> GetFirstAsync(Expression<Func<T, bool>> expr);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expr);

    //Remove
    void Remove(T entity);
    Task RemoveAsync(Guid id);
    void RemoveAll(Expression<Func<T, bool>> expr);

    //Update
    void Update(T entity);

    //Save
    Task<Guid> AddAsync(T entity);
}