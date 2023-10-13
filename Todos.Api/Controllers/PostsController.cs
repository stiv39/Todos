using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Application.Posts.Commands.Create;
using Todos.Application.Posts.Commands.Delete;
using Todos.Application.Posts.Commands.Update;
using Todos.Application.Posts.Queries.Get;
using Todos.Contracts;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly ISender _sender;

    public PostsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts(string? searchTerm, string? sortColumn, string? sortOrder, int page = 1, int pageSize = 100)
    {
        var query = new GetPostsQuery(searchTerm, sortColumn, sortOrder, page, pageSize);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(Guid id)
    {
        var query = new GetPostByIdQuery(id);
        var result = await _sender.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostRequest request)
    {
        var command = new CreatePostCommand(request.Title, request.Body);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePost(UpdatePostRequest request)
    {
        var command = new UpdatePostCommand(request.Id, request.Title, request.Body);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var command = new DeletePostCommand(id);
        var result = await _sender.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }
}
