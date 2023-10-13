using MediatR;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;
using Todos.Domain.Entities;

namespace Todos.Application.Todos.Commands.Update;

internal class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var isSuccess = await _todoRepository.UpdateTodoAsync(new Todo { Id = request.TodoId, IsCompleted = request.IsCompleted, Name = request.Name }, cancellationToken);

        if(isSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        return false;      
    }
}
