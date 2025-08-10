using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Services.Identity;

public class UserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{
    public async Task<ApplicationUser> RegisterUserAsync(string phoneNumber, string firstName, string lastName, string password, string roleName, CancellationToken cancellationToken = default)
    {
        var userFromPhoneNumber = await userManager.FindByNameAsync(phoneNumber);
        if (userFromPhoneNumber is not null)
            throw new BadRequestException(".کاربری با این شماره موبایل در سیستم ثبت شده است");

        var role = await roleManager.FindByNameAsync(roleName);
        if (role is null)
            throw new InvalidOperationException("نقش کاربر در سیستم یافت نشد.");

        ApplicationUser user = new()
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = phoneNumber,
            PhoneNumber = phoneNumber,
            CreatedAt = DateTime.Now,
        };

        var createdUser = await userManager.CreateAsync(user, password);
        if (!createdUser.Succeeded)
        {
            var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"خطا در ایجاد کاربر: {errors}");
        }

        var roleResult = await userManager.AddToRoleAsync(user, role.Name);
        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(user);
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"خطا در تخصیص نقش: {errors}");
        }

        return user;
    }
}
