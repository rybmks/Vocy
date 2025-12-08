namespace Infrastructure.Security;

using Application.Security;
using BCrypt.Net;

public class BCryptPasswordHasher : IPasswordHasher

{
    public string Hash(String input) => BCrypt.HashPassword(input);

    public bool Verify(String input, String hash) => BCrypt.Verify(input, hash);
}