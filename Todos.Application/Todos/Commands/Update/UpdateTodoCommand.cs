using MediatR;

namespace Todos.Application.Todos.Commands.Update;

public sealed record UpdateTodoCommand(Guid TodoId, string Name, bool IsCompleted) : IRequest<bool>;
