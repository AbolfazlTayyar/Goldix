using Goldix.Application.Models.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Persistence.DependencyInjection;

public static class IdentityServiceCollectionExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(ConfigureIdentityOptions);

        services.Configure<JwtOptions>(options =>
            configuration.GetSection("JwtOptions").Bind(options));

        return services;
    }

    private static void ConfigureIdentityOptions(IdentityOptions options)
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;

        // User settings
        options.User.RequireUniqueEmail = false;
    }
}
