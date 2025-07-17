using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Services.Identity;

public class AuthenticationService(UserManager<ApplicationUser> userManager) : IAuthenticationService
{
    public async Task<ApplicationUser> ValidateUserAsync(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);
        if (user is null)
            throw new BadRequestException("Invalid username or password!");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, password);
        if (isPasswordValid is false)
            throw new BadRequestException("Invalid username or password!");

        return user;
    }
}
