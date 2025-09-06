using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Persistence;

namespace Goldix.UnitTests.Helpers.User;

public static class UserTestHelper
{
    public static async Task SeedRolesAsync(ApplicationDbContext db)
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

    public static async Task<List<ApplicationUser>> SeedUsersAsync(ApplicationDbContext db, int count, string status, bool isActive)
    {
        var users = Enumerable.Range(1, count)
            .Select(i => new ApplicationUser
            {
                Id = i.ToString(),
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                PhoneNumber = $"090000000{i:D2}",
                IsActive = isActive,
                Status = status,
                CreatedAt = DateTime.Now,
                GroupId = 1,
            })
            .ToList();

        await db.Users.AddRangeAsync(users);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();

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
