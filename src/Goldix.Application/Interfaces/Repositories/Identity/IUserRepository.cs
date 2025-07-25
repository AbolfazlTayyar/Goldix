using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Repositories.Identity;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<bool> IsUserExistByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<ApplicationUser> CreateUserAsync(string firstName, string lastName, string phoneNumber, string password, CancellationToken cancellationToken = default);
    Task AssignRoleToUser(string userId, IdentityRole role, CancellationToken cancellationToken = default);
}
