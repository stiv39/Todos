

namespace Todos.Contracts.Todos;

public record CreateTodoRequest(Guid UserId, string Name, bool IsCompleted);
