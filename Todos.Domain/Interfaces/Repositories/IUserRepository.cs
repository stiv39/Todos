using Todos.Domain.Entities;

namespace Todos.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<Guid> AddUserAsync(User user);
}
