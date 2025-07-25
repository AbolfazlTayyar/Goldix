using Goldix.Application.Interfaces.Repositories.Identity;
using Goldix.Domain.Entities.Identity;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Services.Identity;

public class UserRepository(ApplicationDbContext db) : IUserRepository
{
    public async Task<ApplicationUser> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var result = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

        return result;
    }
    public async Task<bool> IsUserExistByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var result = await db.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

        return result;
    }
    public async Task<ApplicationUser> CreateUserAsync(string firstName, string lastName, string phoneNumber, string password, CancellationToken cancellationToken = default)
    {
        var fullName = $"{firstName} {lastName}";
        var user = new ApplicationUser
        {
            CreateDate = DateTime.Now,
            FirstName = firstName,
            LastName = lastName,
            UserName = fullName,
            NormalizedUserName = fullName,
            PhoneNumber = phoneNumber,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, password);

        await db.Users.AddAsync(user, cancellationToken);

        return user;
    }
    public async Task AssignRoleToUser(string userId, IdentityRole role, CancellationToken cancellationToken = default)
    {
        IdentityUserRole<string>? userRole = new IdentityUserRole<string>
        {
            RoleId = role.Id,
            UserId = userId
        };

        await db.UserRoles.AddAsync(userRole);
    }
}
