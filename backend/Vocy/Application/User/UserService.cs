using Application.Interfaces;
using Application.Interfaces.Security;
using Application.Security;
using Application.User.Auth;
using Application.User.Commands;
using Domain.Auth;
using Domain.Base;

namespace Application.User;

using Domain.User;

public class UserService(
    IRepository<Domain.User.User> userRepository,
    IRepository<RefreshToken> refreshTokenRepository,
    IPasswordHasher passwordHasher,
    ITokenCreator tokenCreator)
{
    private const int AccessTokenExpirationDurationSecs = 120;
    private const int RefreshTokenExpirationDurationHours = 6;

    //TODO: CHACGE TOKEN HASHER 
    public async Task<AuthResponse> Register(RegisterUserCommand registerUserCommand)
    {
        User user = new(registerUserCommand.Name, registerUserCommand.Email,
            passwordHasher.Hash(registerUserCommand.Password));

        var userId = await userRepository.Save(user);

        var (accessToken, refreshToken) = await this.CreateAndSaveTokens(userId);
        return new AuthResponse(userId, accessToken, refreshToken);
    }

    public async Task<AuthResponse> LogIn(LoginUserCommand loginUserCommand)
    {
        var user = await userRepository.GetFirstOrDefault(u => u.Email == loginUserCommand.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!passwordHasher.Verify(loginUserCommand.Password, user.Password))
        {
            throw new Exception("Incorrect password");
        }

        var (accessToken, refreshToken) = await this.CreateAndSaveTokens(user.Id);

        return new AuthResponse(user.Id, accessToken, refreshToken);
    }

    public async Task<AuthResponse> Refresh(RefreshTokenCommand refreshTokenCommand)
    {
        var oldRefreshTokenHash = tokenCreator.HashToken(refreshTokenCommand.RefreshToken);
        var oldRefreshToken = await refreshTokenRepository.GetFirstOrDefault(t => oldRefreshTokenHash == t.TokenHash);

        if (!oldRefreshToken.IsActive)
        {
            throw new Exception();
        }

        oldRefreshToken.RevokedAt = DateTime.UtcNow;

        var (accessToken, refreshTokenValue) = await this.CreateAndSaveTokens(oldRefreshToken.UserId);

        return new AuthResponse(oldRefreshToken.UserId, accessToken, refreshTokenValue);
    }

    private async Task<(String, String)> CreateAndSaveTokens(Guid userId)
    {
        var claims = new TokenClaims(userId);
        var accessToken =
            tokenCreator.CreateAccessToken(claims, DateTime.UtcNow.AddSeconds(AccessTokenExpirationDurationSecs));

        var refreshTokenValue = tokenCreator.CreateRefreshToken();
        var refreshTokenHash = tokenCreator.HashToken(refreshTokenValue);

        var refreshToken = new RefreshToken(userId, refreshTokenHash,
            DateTime.UtcNow.AddHours(RefreshTokenExpirationDurationHours), DateTime.UtcNow);

        await refreshTokenRepository.Save(refreshToken);

        return (accessToken, refreshTokenValue);
    }
}