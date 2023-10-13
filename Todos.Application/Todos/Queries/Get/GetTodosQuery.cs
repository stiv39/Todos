using MediatR;
using Todos.Application.Todos.Shared;

namespace Todos.Application.Todos.Queries.Get;

public record GetTodosQuery(string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize): IRequest<TodosResponse>;
