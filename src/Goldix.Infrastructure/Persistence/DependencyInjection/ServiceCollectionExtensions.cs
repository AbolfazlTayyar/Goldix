using Goldix.Application.Interfaces.Infrastructure;

namespace Goldix.Infrastructure.Persistence.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMarkedServices(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementationAssembly)
    {
        RegisterServicesByMarker<IScopedService>(services, interfaceAssembly, implementationAssembly, ServiceLifetime.Scoped);
        RegisterServicesByMarker<ISingletonService>(services, interfaceAssembly, implementationAssembly, ServiceLifetime.Singleton);
        RegisterServicesByMarker<ITransientService>(services, interfaceAssembly, implementationAssembly, ServiceLifetime.Transient);

        return services;
    }

    private static void RegisterServicesByMarker<TMarker>(IServiceCollection services,
        Assembly interfaceAssembly, Assembly implementationAssembly, ServiceLifetime lifetime) where TMarker : IService
    {
        var serviceInterfaces = interfaceAssembly.GetTypes()
            .Where(t => t.IsInterface &&
                       typeof(TMarker).IsAssignableFrom(t) &&
                       t != typeof(TMarker) &&
                       t != typeof(IService))
            .ToList();

        foreach (var serviceInterface in serviceInterfaces)
        {
            var implementation = implementationAssembly.GetTypes()
                .FirstOrDefault(t => t.IsClass &&
                               !t.IsAbstract &&
                               serviceInterface.IsAssignableFrom(t));

            if (implementation is not null)
                services.Add(new ServiceDescriptor(serviceInterface, implementation, lifetime));
        }
    }
}
