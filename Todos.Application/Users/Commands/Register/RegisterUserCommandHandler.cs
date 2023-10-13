using MediatR;
using Todos.Domain.Entities;
using Todos.Domain.Shared;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;

namespace Todos.Application.Users.Commands.Register;

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    { 
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

        if(existingUser != null)
        {
            return Result<Guid>.Failure(new Error("Err", "Email already in use"));
        }

        var id = Guid.NewGuid();

        var user = new User
        {
            Id = id,
            Name = request.Name,
            Email = request.Email,
            PasswordHash = request.Password, // TODO
            PasswordSalt = "" // TODO
        };

        await _userRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(id);
    }
}
