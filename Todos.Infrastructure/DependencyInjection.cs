using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todos.Infrastructure.Persistence;

namespace Todos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(assembly.FullName));
        });

        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<ITodosRepository, TodosRepository>();
        //services.AddScoped<IPostsRepository, PostsRepository>();

        return services;
    }
}
