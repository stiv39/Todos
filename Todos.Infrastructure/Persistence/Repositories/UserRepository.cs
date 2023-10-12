using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;

namespace Todos.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task<Guid> AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
