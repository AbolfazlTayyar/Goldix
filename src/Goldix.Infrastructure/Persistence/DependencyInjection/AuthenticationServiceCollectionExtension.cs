using Microsoft.AspNetCore.Authentication;

namespace Goldix.Infrastructure.Persistence.DependencyInjection;

public static class AuthenticationServiceCollectionExtension
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(ConfigureAuthenticationOptions)
                .AddJwtBearer(options => ConfigureJwtBearerOptions(options, configuration));

        return services;
    }

    private static void ConfigureAuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    private static void ConfigureJwtBearerOptions(JwtBearerOptions options, IConfiguration configuration)
    {
        options.RequireHttpsMetadata = false; // Set true in production
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuration["JwtOptions:Issuer"],
            ValidAudience = configuration["JwtOptions:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JwtOptions:Key"]))
        };
    }
}
