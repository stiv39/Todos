using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Application.Users.Commands.Register;
using Todos.Contracts.Users;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var command = new RegisterUserCommand(request.Name, request.Email, request.Password);

        var result = await _sender.Send(command);

        if(!result.IsSuccess)
        {
            BadRequest("Email already in use");
        }

        return Ok(result.Value);
    }
}
