using MediatR;
using Todos.Domain.Interfaces.Repositories;
using Todos.Application.Posts.Queries.Get;
using Todos.Domain.Entities;

namespace Todos.Application;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post?>
{
    private readonly IPostRepository _postRepository;

    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken);
    }
}
