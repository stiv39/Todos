using MediatR;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Application.Posts.Commands.Delete;

internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var postFromDb = await _postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (postFromDb == null)
        {
            return false;
        }

        _postRepository.DeletePost(postFromDb);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
