using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Constants;

namespace Goldix.Infrastructure.Persistence.Extensions;

public class DbInitializer
{
    public static async Task ApplyMigrationsIfPending(ApplicationDbContext db)
    {
        try
        {
            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations?.Any() == true)
                await db.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static async Task SeedIdentity(ApplicationDbContext db, IUserService userService)
    {
        if (db.Roles.Any())
            return;

        using var transaction = db.Database.BeginTransaction();

        try
        {
            await AddRoles(db);

            var superAdminUser = await userService.RegisterUserAsync("09372144430", "ابوالفضل", "طیار", "@Bolfazl0916350", RoleConstants.SUPER_ADMIN);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private static async Task AddRoles(ApplicationDbContext db)
    {
        var roles = new[]
        {
            CreateRole(RoleConstants.SUPER_ADMIN),
            CreateRole(RoleConstants.ADMIN),
            CreateRole(RoleConstants.USER),
        };

        db.Roles.AddRange(roles);
        await db.SaveChangesAsync();
    }

    private static IdentityRole CreateRole(string roleName)
    {
        return new IdentityRole
        {
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
    }
}
