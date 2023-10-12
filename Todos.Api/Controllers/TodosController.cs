using Microsoft.AspNetCore.Mvc;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodo()
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(Guid id)
    {
        return Ok();
    }
}
