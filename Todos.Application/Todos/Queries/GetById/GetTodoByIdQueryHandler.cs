using MediatR;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;

namespace Todos.Application.Todos.Queries.GetById;

internal class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, Todo?>
{
    private readonly ITodoRepository _repository;

    public GetTodoByIdQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Todo?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todoFromDb = await _repository.GetTodoByIdAsync(request.Id, cancellationToken);

        return todoFromDb;
    }
}
