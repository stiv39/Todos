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
    Task<Todo?> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddTodoAsync(Todo todo, CancellationToken cancellationToken);
    Task<bool> UpdateTodoAsync(Todo todo, CancellationToken cancellationToken);
    void DeleteTodo(Todo todo);
}
