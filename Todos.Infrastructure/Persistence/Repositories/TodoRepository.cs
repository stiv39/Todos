using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;

namespace Todos.Infrastructure.Persistence.Repositories;

public class TodoRepository : ITodoRepository
{
    public Task<Guid> AddTodoAsync(Todo Todo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void DeleteTodo(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Todo> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Todo>> GetTodosAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Todo>> GetTodosByIdsAsync(List<Guid> TodoIds, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Todo?> UpdateTodoAsync(Todo Todo, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
