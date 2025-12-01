namespace Application.User;

public class UserService(IUserRepository userRepository)
{
    public async Task CreateUser(Domain.User.User user)
    {
        await userRepository.CreateUser(user);
    }
}