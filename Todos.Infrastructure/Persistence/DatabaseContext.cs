using Microsoft.EntityFrameworkCore;
using Todos.Domain.Entities;

namespace Todos.Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {           
    }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}
