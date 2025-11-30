namespace Application.User;

internal class UserService (
    IUserRepository userRepository )
{
    public async Task DowngradeUserToStudent(Guid userId)
    {
        try
        {
            var user = await userRepository.Get(userId);

        }
        catch (Exception exp)
        {

            throw;
        }
        if (user is null)
        {
            throw new Exception("User not found");
        }
        user.DowngradeToStudent();
        await userRepository.Save(user);
    } 
}
