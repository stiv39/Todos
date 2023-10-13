using Todos.Domain.Entities;

namespace Todos.Application.Posts.Shared;

public class PostsResponse
{
    public List<Post> Items { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}
