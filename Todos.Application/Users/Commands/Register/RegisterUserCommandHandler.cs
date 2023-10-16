using MediatR;
using Todos.Domain.Entities;
using Todos.Domain.Shared;
using Todos.Domain.Interfaces.Repositories;
using Todos.Domain.Interfaces.UnitOfWork;
using Todos.Application.Common.Services;

namespace Todos.Application.Users.Commands.Register;

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordService _passwordService;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    { 
        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);

        if(existingUser != null)
        {
            var a = _passwordService.VerifyPassword(existingUser.Id, request.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

            return Result<Guid>.Failure(new Error("Err", "Email already in use"));
        }

        var id = Guid.NewGuid();
        var password = _passwordService.EncryptPassword(id, request.Password, out var salt);
        var user = new User
        {
            Id = id,
            Name = request.Name,
            Email = request.Email,
            PasswordHash = password,
            PasswordSalt = salt
        };

        await _userRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result<Guid>.Success(id);
    }
}
