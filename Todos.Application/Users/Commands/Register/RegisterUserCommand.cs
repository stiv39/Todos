using MediatR;
using Todos.Domain.Shared;

namespace Todos.Application.Users.Commands.Register;

public sealed record RegisterUserCommand(string Name, string Email, string Password) : IRequest<Result<Guid>>;
