using Application.User;
using Microsoft.AspNetCore.Mvc;
using Domain.User;

namespace Api.Controllers;

public class UserController(IUserRepository repository) : Controller
{
    private readonly IUserRepository _repository = repository;

    [HttpPost("create")]
    public IActionResult Create(User user)
    {
        _repository.CreateUser(user);

        return Accepted();
    }
}