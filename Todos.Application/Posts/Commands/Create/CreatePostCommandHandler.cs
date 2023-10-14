using MediatR;
using Todos.Domain.Interfaces.Repositories; 
using Todos.Domain.Interfaces.UnitOfWork;
using Todos.Domain.Entities;
using Todos.Domain.Shared;

namespace Todos.Application.Posts.Commands.Create;

internal class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<Guid>>
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newPost = new Post { Id = Guid.NewGuid(), Body = request.Body, Title = request.Title };

            await _postRepository.AddPostAsync(newPost, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(newPost.Id);
        }
        catch(Exception ex)
        {
            return Result<Guid>.Failure(new Error("err", ex.Message));
        }
    }
}
