using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Services.Identity;

public class AuthenticationService(UserManager<ApplicationUser> userManager, ApplicationDbContext db) : IAuthenticationService
{
    public async Task<ApplicationUser> ValidateUserAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await db.Users.FirstOrDefaultAsync(x => x.PhoneNumber == username, cancellationToken);
        if (user is null)
            throw new BadRequestException("Invalid username or password!");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, password);
        if (isPasswordValid is false)
            throw new BadRequestException("Invalid username or password!");

        return user;
    }
}
