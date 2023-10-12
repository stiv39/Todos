using Microsoft.AspNetCore.Mvc;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Register(string email, string password)
    {
        return Ok();
    }
}
