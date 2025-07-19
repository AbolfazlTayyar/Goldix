using Goldix.Infrastructure.Persistence;
using Goldix.Infrastructure.Persistence.Extensions;

namespace Goldix.API.Extensions;

public static class DbExtensions
{
    public static async Task ApplyMigrationsIfPending(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await DbInitializer.ApplyMigrationsIfPending(context);
    }
}