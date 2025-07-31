using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Constants;

namespace Goldix.Infrastructure.Persistence;

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

    public static async Task SeedIdentity(RoleManager<IdentityRole> roleManager, IUserService userService)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole { Name = RoleConstants.SUPER_ADMIN, NormalizedName = RoleConstants.SUPER_ADMIN.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = RoleConstants.ADMIN, NormalizedName = RoleConstants.ADMIN.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = RoleConstants.USER, NormalizedName = RoleConstants.USER.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() });

            await userService.RegisterUserAsync("09372144430", "ابوالفضل", "طیار", "@Bolfazl0916350", RoleConstants.SUPER_ADMIN);
            await userService.RegisterUserAsync("09163503284", "عبدی", "طیار", "@Bolfazl0916350", RoleConstants.ADMIN);
            await userService.RegisterUserAsync("09163081897", "سهیلا", "بویری", "@Bolfazl0916350", RoleConstants.ADMIN);
        }
    }
}
