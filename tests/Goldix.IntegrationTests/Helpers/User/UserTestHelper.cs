using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.WalletManagement;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.User;

public static class UserTestHelper
{
    public static async Task CreateRolesAsync(ApplicationDbContext db)
    {
        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id = "1", Name = RoleConstants.USER, NormalizedName = RoleConstants.USER.ToUpper() },
            new IdentityRole { Id = "2", Name = RoleConstants.ADMIN, NormalizedName = RoleConstants.ADMIN.ToUpper() },
            new IdentityRole { Id = "3", Name = RoleConstants.SUPER_ADMIN, NormalizedName = RoleConstants.SUPER_ADMIN.ToUpper() },
        };

        await db.Roles.AddRangeAsync(roles);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }

    public static async Task<List<ApplicationUser>> CreateUsersAsync(ApplicationDbContext db, int count, string status = null, bool isActive = true, bool shouldHaveWallet = false)
    {
        var users = Enumerable.Range(1, count)
            .Select(i => new ApplicationUser
            {
                Id = i.ToString(),
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                PhoneNumber = $"090000000{i:D2}",
                UserName = $"090000000{i:D2}",
                IsActive = isActive,
                Status = status ?? UserStatus.confirmed.ToDisplay(),
                CreatedAt = DateTime.Now,
                GroupId = 1,
                Wallet = shouldHaveWallet == true ?
                new Wallet
                {
                    UserId = i.ToString(),
                    Balance = i * 100000,
                    CreatedAt = DateTime.Now
                } : null
            })
            .ToList();

        await db.Users.AddRangeAsync(users);
        await db.SaveChangesAsync();

        return users;
    }

    public static async Task AddUsersToRole(ApplicationDbContext db, List<ApplicationUser> users, string role)
    {
        var foundedRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == role);

        db.UserRoles.AddRange(users.Select(u => new IdentityUserRole<string>
        {
            UserId = u.Id,
            RoleId = foundedRole.Id
        }));

        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }
}
