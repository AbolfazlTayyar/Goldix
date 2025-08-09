using Goldix.Application.Behaviors;

namespace Goldix.Infrastructure.Persistence.DependencyInjection;

public static class MediatRServiceCollectionExtension
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        }).AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}

