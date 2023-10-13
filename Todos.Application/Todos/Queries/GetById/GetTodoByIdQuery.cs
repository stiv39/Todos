using MediatR;
using Todos.Domain.Entities;

namespace Todos.Application.Todos.Queries.GetById;

public sealed record GetTodoByIdQuery(Guid Id) : IRequest<Todo?>;
