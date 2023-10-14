using MediatR;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Application.Posts.Commands.Update;

internal class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
       var result = await _postRepository.UpdatePostAsync(new Post { Id = request.Id, Body = request.Body, Title = request.Title }, cancellationToken);

        if(result)
        {
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
