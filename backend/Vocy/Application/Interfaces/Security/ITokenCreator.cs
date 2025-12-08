using Application.User.Auth;

namespace Application.Interfaces.Security;

public interface ITokenCreator
{
    String CreateAccessToken(TokenClaims claims, DateTime expiresIn);
    String CreateRefreshToken();
    String HashToken(string input);
}