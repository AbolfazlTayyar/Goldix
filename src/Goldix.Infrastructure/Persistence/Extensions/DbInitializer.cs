namespace Goldix.Infrastructure.Persistence.Extensions;

public class DbInitializer
{
    public static async Task ApplyMigrationsIfPending(ApplicationDbContext context)
    {
        try
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations?.Any() == true)
                await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
