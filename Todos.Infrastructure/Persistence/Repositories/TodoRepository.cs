using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;

namespace Todos.Infrastructure.Persistence.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly DatabaseContext _databaseContext;

    public TodoRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddTodoAsync(Todo Todo, CancellationToken cancellationToken)
    {
        await _databaseContext.Todos.AddAsync(Todo, cancellationToken);
    }

    public void DeleteTodo(Todo todo)
    {
        _databaseContext.Todos.Remove(todo);
    }

    public async Task<Todo?> GetTodoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _databaseContext.Todos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Todo>> GetTodosAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken)
    {
        IQueryable<Todo> todosQuery = _databaseContext.Todos;

        var totalCount = todosQuery.Count();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            todosQuery = todosQuery.Where(m => m.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        if (sortOrder == "desc")
        {
            todosQuery = todosQuery.OrderByDescending(GetSortColumn(sortColumn));
        }
        else
        {
            todosQuery = todosQuery.OrderBy(GetSortColumn(sortColumn));
        }

        return await todosQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateTodoAsync(Todo todo, CancellationToken cancellationToken)
    {
        var todoFromDb = await _databaseContext.Todos.FirstOrDefaultAsync(t => t.Id == todo.Id, cancellationToken);

        if (todoFromDb != null)
        {
            todoFromDb.Name = todo.Name;
            todoFromDb.IsCompleted = todo.IsCompleted;

            return true;
        }

        return false;
    }

    private static Expression<Func<Todo, object>> GetSortColumn(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "name" => todo => todo.Name,
            "iscompleted" => todo => todo.IsCompleted,
            _ => todo => todo.Id
        };
    }
}
