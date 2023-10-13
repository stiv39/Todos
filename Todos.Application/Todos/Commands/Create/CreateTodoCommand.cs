using MediatR;
using Todos.Domain.Shared;

namespace Todos.Application.Todos.Commands.Create;

public sealed record CreateTodoCommand(string Name, bool IsCompleted, Guid UserId) : IRequest<Result<Guid>>;
