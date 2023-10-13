namespace Todos.Contracts.Todos;

public record UpdateTodoRequest(Guid TodoId, string Name, bool IsCompleted);
