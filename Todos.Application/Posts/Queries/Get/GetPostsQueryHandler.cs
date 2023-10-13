using MediatR;
using Posts.Domain.Interfaces.Repositories;
using Todos.Application.Posts.Shared;

namespace Todos.Application.Posts.Queries.Get;

internal class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PostsResponse>
{
    private readonly IPostRepository _postRepository;

    public GetPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostsResponse> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var postsFromDb = await _postRepository.GetPostsAsync(request.SearchTerm, request.SortColumn, request.SortOrder, request.Page, request.PageSize, cancellationToken);

        return new PostsResponse
        {
            TotalCount = postsFromDb.Count,
            Items = postsFromDb,
            Page = request.Page,
            PageSize = request.PageSize,
        };
    }
}
