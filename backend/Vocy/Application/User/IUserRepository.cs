namespace Application.User;

using Domain.User;

public interface IUserRepository
{
    public void CreateUser(User user);

    public void RemoveUser(Guid userId);
}