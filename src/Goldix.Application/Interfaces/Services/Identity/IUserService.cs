using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.User;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IUserService : IScopedService
{
    Task<ApplicationUser> RegisterUserAsync(string phoneNumber, string firstName, string lastName,
        string password, string roleName, UserStatus status = UserStatus.waiting,
        CancellationToken cancellationToken = default);

    string GetCurrentUserId();
}
