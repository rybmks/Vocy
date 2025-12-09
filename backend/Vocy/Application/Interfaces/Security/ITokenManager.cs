using Application.Auth.Helpers;

namespace Application.Interfaces.Security;

public interface ITokenManager
{
    String CreateAccessToken(TokenClaims claims, DateTime expiresIn);
    TokenClaims ParseAccessToken(string token);
    String CreateRefreshToken();
    String HashToken(string input);
}