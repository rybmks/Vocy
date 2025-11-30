namespace Application.User;

using Domain.User;

public interface Repository<T> : IRepository<T> where T : BaseIdentity
{

}