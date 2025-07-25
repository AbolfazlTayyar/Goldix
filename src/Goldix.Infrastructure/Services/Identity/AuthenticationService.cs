using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Repositories.Common;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Services.Identity;

public class AuthenticationService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IAuthenticationService
{
    public async Task<ApplicationUser> ValidateUserAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByPhoneNumberAsync(username, cancellationToken);
        if (user is null)
            throw new BadRequestException("Invalid username or password!");

        var isPasswordValid = await userManager.CheckPasswordAsync(user, password);
        if (isPasswordValid is false)
            throw new BadRequestException("Invalid username or password!");

        return user;
    }
}
