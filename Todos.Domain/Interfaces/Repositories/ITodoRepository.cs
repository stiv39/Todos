using Todos.Domain.Entities;

namespace Todos.Domain.Interfaces.Repositories;

public interface ITodoRepository
{
    Task<List<Todo>> GetTodosAsync(
    string? searchTerm,
    string? sortColumn,
    string? sortOrder,
    int page,
    int pageSize,
    CancellationToken cancellationToken);
    Task<List<Todo>> GetTodosByIdsAsync(List<Guid> TodoIds, CancellationToken cancellationToken);
    Task<Todo> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> AddTodoAsync(Todo Todo, CancellationToken cancellationToken);
    Task<Todo?> UpdateTodoAsync(Todo Todo, CancellationToken cancellationToken);
    void DeleteTodo(Guid id, CancellationToken cancellationToken);
}
