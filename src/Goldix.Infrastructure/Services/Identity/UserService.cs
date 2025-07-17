using Goldix.Application.Interfaces.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Services.Identity;

public class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<bool> ExistsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        var result = await userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

        return result;
    }
}
