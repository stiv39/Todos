using Microsoft.AspNetCore.Mvc;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost()
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePost(Guid id)
    {
        return Ok();
    }
}
