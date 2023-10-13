using MediatR;

namespace Todos.Application.Todos.Commands.Delete;

public sealed record DeleteTodoCommand(Guid TodoId) : IRequest<bool>;
