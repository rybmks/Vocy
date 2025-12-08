using Application.Interfaces;
using Application.Interfaces.Security;
using Application.User.Commands;
using Domain.Auth;

namespace Application.User.Auth;

public interface ITokenService
{
    Task<AuthResponse> Refresh(RefreshTokenCommand refreshTokenCommand);
    Task<(String, String)> CreateAndSaveTokens(Guid userId);
}

internal class TokenService(
    IRepository<RefreshToken> refreshTokenRepository,
    ITokenCreator tokenCreator) : ITokenService
{
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

    private readonly TimeSpan AccessTokenExpirationDuration = TimeSpan.FromSeconds(120);
    private readonly TimeSpan RefreshTokenExpirationDuration = TimeSpan.FromHours(6);
    public async Task<(String, String)> CreateAndSaveTokens(Guid userId)
    {
        var claims = new TokenClaims(userId);
        var accessToken = tokenCreator.CreateAccessToken(claims, DateTime.UtcNow.AddTicks(AccessTokenExpirationDuration.Ticks));

        var refreshTokenValue = tokenCreator.CreateRefreshToken();
        var refreshTokenHash = tokenCreator.HashToken(refreshTokenValue);
        var refreshToken = new RefreshToken(userId, refreshTokenHash,
            DateTime.UtcNow.AddTicks(RefreshTokenExpirationDuration.Ticks), DateTime.UtcNow);

        await refreshTokenRepository.Save(refreshToken);

        return (accessToken, refreshTokenValue);
    }
}
