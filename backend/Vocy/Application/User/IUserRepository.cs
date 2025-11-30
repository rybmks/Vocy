namespace Application.User;

using Domain.User;

public interface IUserRepository
{
    Task CreateUser(User user);

    Task RemoveUser(Guid userId);
}