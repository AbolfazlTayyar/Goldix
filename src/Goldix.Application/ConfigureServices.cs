using Goldix.Application.Validators.Configurations;

namespace Goldix.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        ValidatorOptions.Global.LanguageManager = new FluentValidationCustomLanguageManager();
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
