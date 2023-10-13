using MediatR;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Application.Todos.Commands.Delete;

internal class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todoFromDb = await _todoRepository.GetTodoByIdAsync(request.TodoId, cancellationToken);

        if (todoFromDb == null)
        {
            return false;
        }

        _todoRepository.DeleteTodo(todoFromDb);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
