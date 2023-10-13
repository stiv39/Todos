using MediatR;
using Todos.Domain.Shared;

namespace Todos.Application.Posts.Commands.Create;

public sealed record CreatePostCommand(string Title, string Body) : IRequest<Result<Guid>>;
