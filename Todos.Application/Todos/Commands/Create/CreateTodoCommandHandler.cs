using MediatR;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;
using Todos.Domain.Shared;

namespace Todos.Application.Todos.Commands.Create;

internal class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Result<Guid>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoCommandHandler(ITodoRepository todoRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await _userRepository.GetUserByIdAsync(request.UserId);

        if(userFromDb == null) 
        { 
            return Result<Guid>.Failure(new Error("Wrong user", "User does not exist"));      
        }

        var newId = Guid.NewGuid();
        var newTodo = new Todo() { Id = newId, IsCompleted = request.IsCompleted, Name = request.Name, UserId = request.UserId, User = userFromDb };

        await _todoRepository.AddTodoAsync(newTodo, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(newId);
    }
}
