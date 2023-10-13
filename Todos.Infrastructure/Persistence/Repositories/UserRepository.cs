using Microsoft.EntityFrameworkCore;
using Todos.Domain.Entities;
using Todos.Domain.Interfaces.Repositories;

namespace Todos.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddUserAsync(User user)
    {
        await _databaseContext.Users.AddAsync(user);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _databaseContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
