using Todos.Domain.Entities;

namespace Todos.Application.Todos.Shared;

public class TodosResponse
{
    public List<Todo> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}
