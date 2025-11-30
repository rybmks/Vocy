using Application.User;
using Microsoft.AspNetCore.Mvc;
using Domain.User;

namespace Api.Controllers;

public class UserController(IUserRepository repository) : Controller
{
    private readonly IUserRepository _repository = repository;

    [HttpPost("create")]
    public async Task Create(User user)
    {
        await _repository.Save(user);
    }
}