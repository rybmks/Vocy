namespace Application.Security;

public interface IPasswordHasher
{
    String Hash(String input);
    bool Verify(String input, String hash);
}