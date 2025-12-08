using Application.Interfaces;
using Application.Interfaces.Security;
using Application.Security;
using Application.User.Auth;
using Application.User.Commands;
using Domain.Auth;
using Domain.Base;

namespace Application.User;

using Domain.User;

public class AuthService(
    IRepository<Domain.User.User> userRepository,
    ITokenService tokenService,
    IPasswordHasher passwordHasher)
{
    public async Task<AuthResponse> Register(RegisterUserCommand registerUserCommand)
    {
        User user = new(registerUserCommand.Name, registerUserCommand.Email,
            passwordHasher.Hash(registerUserCommand.Password));

        var userId = await userRepository.Save(user);

        var (accessToken, refreshToken) = await tokenService.CreateAndSaveTokens(userId);
        return new AuthResponse(userId, accessToken, refreshToken);
    }

    public async Task<AuthResponse> LogIn(LoginUserCommand loginUserCommand)
    {
        var user = await userRepository.GetFirstOrDefault(u => u.Email == loginUserCommand.Email)
            ?? throw new Exception("User not found");

        if (!passwordHasher.Verify(loginUserCommand.Password, user.Password))
        {
            throw new Exception("Incorrect password");
        }

        var (accessToken, refreshToken) = await tokenService.CreateAndSaveTokens(user.Id);
        return new AuthResponse(user.Id, accessToken, refreshToken);
    }
}