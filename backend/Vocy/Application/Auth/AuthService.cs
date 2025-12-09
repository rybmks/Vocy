using Application.Auth.Commands;
using Application.Auth.Helpers;
using Application.Interfaces;
using Application.Interfaces.Security;
using Application.Security;
using Domain.Auth;

namespace Application.Auth;

public class AuthService(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    ITokenManager tokenManager)
{
    private const int AccessTokenExpirationDurationSecs = 120;
    private const int RefreshTokenExpirationDurationHours = 6;

    private readonly IRepository<Domain.User.User> _userRepository = unitOfWork.GetRepository<Domain.User.User>();
    private readonly IRepository<RefreshToken> _refreshTokenRepository = unitOfWork.GetRepository<RefreshToken>();


    public async Task<AuthResponse> Register(RegisterUserCommand registerUserCommand)
    {
        Domain.User.User user = new(registerUserCommand.Name, registerUserCommand.Email,
            passwordHasher.Hash(registerUserCommand.Password));

        var userId = await _userRepository.AddAsync(user);

        var (accessToken, refreshToken) = await this.CreateAndAddTokens(userId);

        await unitOfWork.SaveChangesAsync();
        return new AuthResponse(userId, accessToken, refreshToken);
    }

    public async Task<AuthResponse> LogIn(LoginUserCommand loginUserCommand)
    {
        var user = await _userRepository.GetFirstAsync(u => u.Email == loginUserCommand.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!passwordHasher.Verify(loginUserCommand.Password, user.Password))
        {
            throw new Exception("Incorrect password");
        }

        var (accessToken, refreshToken) = await this.CreateAndAddTokens(user.Id);

        await unitOfWork.SaveChangesAsync();
        return new AuthResponse(user.Id, accessToken, refreshToken);
    }

    public async Task<AuthResponse> Refresh(RefreshTokenCommand refreshTokenCommand)
    {
        var oldRefreshTokenHash = tokenManager.HashToken(refreshTokenCommand.RefreshToken);
        var oldRefreshToken = await _refreshTokenRepository.GetFirstAsync(t => oldRefreshTokenHash == t.TokenHash);

        if (!oldRefreshToken.IsActive)
        {
            throw new Exception();
        }

        oldRefreshToken.RevokedAt = DateTime.UtcNow;

        var (accessToken, refreshTokenValue) = await this.CreateAndAddTokens(oldRefreshToken.UserId);

        await unitOfWork.SaveChangesAsync();
        return new AuthResponse(oldRefreshToken.UserId, accessToken, refreshTokenValue);
    }

    private async Task<(String, String)> CreateAndAddTokens(Guid userId)
    {
        var claims = new TokenClaims(userId);
        var accessToken =
            tokenManager.CreateAccessToken(claims, DateTime.UtcNow.AddSeconds(AccessTokenExpirationDurationSecs));

        var refreshTokenValue = tokenManager.CreateRefreshToken();
        var refreshTokenHash = tokenManager.HashToken(refreshTokenValue);

        var refreshToken = new RefreshToken(userId, refreshTokenHash,
            DateTime.UtcNow.AddHours(RefreshTokenExpirationDurationHours), DateTime.UtcNow);

        await _refreshTokenRepository.AddAsync(refreshToken);

        return (accessToken, refreshTokenValue);
    }
}