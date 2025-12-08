using Application.User;
using Application.User.Auth;
using Application.User.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api")]
public class UserController(AuthService userService) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand loginUserCommand)
    {
        var serviceResponse = await userService.Register(loginUserCommand);
        Response.Cookies.Append("access", serviceResponse.AccessToken);
        Response.Cookies.Append("refresh", serviceResponse.RefreshToken);

        return Accepted();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LoginUserCommand loginUserCommand)
    {
        var serviceResponse = await userService.LogIn(loginUserCommand);
        Response.Cookies.Append("access", serviceResponse.AccessToken);
        Response.Cookies.Append("refresh", serviceResponse.RefreshToken);

        return Accepted();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh"];

        if (refreshToken == null)
        {
            throw new Exception("no refresh");
        }

        var command = new RefreshTokenCommand(refreshToken);
        var serviceResponse = await userService.Refresh(command);

        Response.Cookies.Append("access", serviceResponse.AccessToken);
        Response.Cookies.Append("refresh", serviceResponse.RefreshToken);

        return Accepted();
    }
}