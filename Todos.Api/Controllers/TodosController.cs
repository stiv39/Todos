using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Application.Todos.Commands.Create;
using Todos.Application.Todos.Commands.Delete;
using Todos.Application.Todos.Commands.Update;
using Todos.Application.Todos.Queries.Get;
using Todos.Application.Todos.Queries.GetById;
using Todos.Contracts.Todos;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ISender _sender;

    public TodosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos(string? searchTerm, string? sortColumn, string? sortOrder, int page = 1, int pageSize = 100)
    {
        var query = new GetTodosQuery(searchTerm, sortColumn, sortOrder, page, pageSize);
        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(Guid id)
    {
        var query = new GetTodoByIdQuery(id);
        var result = await _sender.Send(query);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(request.Name, request.IsCompleted, request.UserId);
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodo(UpdateTodoRequest request)
    {
        var command = new UpdateTodoCommand(request.TodoId, request.Name, request.IsCompleted);
        var result = await _sender.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        var command = new DeleteTodoCommand(id);
        var result = await _sender.Send(command);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }
}
