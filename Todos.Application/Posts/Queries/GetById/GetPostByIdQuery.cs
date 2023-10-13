using MediatR;
using Todos.Domain.Entities;

namespace Todos.Application.Posts.Queries.Get;

public record GetPostByIdQuery(Guid PostId) : IRequest<Post?>;
