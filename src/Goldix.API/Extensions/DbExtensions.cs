using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Infrastructure.Persistence;
using Goldix.Infrastructure.Persistence.Extensions;

namespace Goldix.API.Extensions;

public static class DbExtensions
{
    public static async Task ApplyMigrationsIfPending(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await DbInitializer.ApplyMigrationsIfPending(db);
    }

    public static async Task SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        await DbInitializer.SeedIdentity(roleManager, userService);
    }
}