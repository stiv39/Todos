using MediatR;
using Todos.Application.Todos.Shared;
using Todos.Domain.Interfaces.Repositories;

namespace Todos.Application.Todos.Queries.Get;

internal class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosResponse>
{
    private readonly ITodoRepository _todoRepository;

    public GetTodosQueryHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodosResponse> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todosFromDb = await _todoRepository.GetTodosAsync(request.SearchTerm, request.SortColumn, request.SortOrder, request.Page, request.PageSize, cancellationToken);

        return new TodosResponse
        {
            TotalCount = todosFromDb.Count,
            Items = todosFromDb,
            Page = request.Page,
            PageSize = request.PageSize,
        };
    }
}
