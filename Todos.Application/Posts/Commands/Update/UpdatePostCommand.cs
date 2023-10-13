using MediatR;

namespace Todos.Application.Posts.Commands.Update;

public sealed record UpdatePostCommand(Guid Id, string Title, string Body) : IRequest<bool>;
