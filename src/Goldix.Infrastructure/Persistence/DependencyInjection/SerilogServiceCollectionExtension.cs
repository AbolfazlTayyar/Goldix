namespace Goldix.Infrastructure.Persistence.DependencyInjection;

public static class SerilogServiceCollectionExtension
{
    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return services;
    }
}
