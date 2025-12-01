using Application.User;
using Microsoft.AspNetCore.Mvc;
using Domain.User;

namespace Api.Controllers;

public class UserController(UserService userService) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        await userService.CreateUser(user);
        return Accepted();
    }
}