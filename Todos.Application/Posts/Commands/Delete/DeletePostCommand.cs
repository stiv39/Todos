using MediatR;

namespace Todos.Application.Posts.Commands.Delete;

public sealed record DeletePostCommand(Guid PostId) : IRequest<bool>;

