using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Infrastructure.Persistence.DependencyInjection;

namespace Goldix.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration)
            .AddMarkedServices(typeof(IValidationService).Assembly, Assembly.GetExecutingAssembly())
            .AddMediatRServices()
            .AddIdentityServices(configuration)
            .AddAuthenticationServices(configuration);

        return services;
    }
}
