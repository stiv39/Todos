using MediatR;
using Todos.Application.Posts.Shared;

namespace Todos.Application.Posts.Queries.Get;


public record GetPostsQuery(string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<PostsResponse>;
