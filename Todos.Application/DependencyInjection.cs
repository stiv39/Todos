using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Todos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        //services.AddFluentValidationAutoValidation()
        //       .AddFluentValidationClientsideAdapters()
        //       .AddValidatorsFromAssembly(assembly);


        return services;
    }
}
