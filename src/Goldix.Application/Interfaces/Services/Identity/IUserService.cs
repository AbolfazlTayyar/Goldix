using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IUserService
{
    Task<ApplicationUser> RegisterUserAsync(string phoneNumber, string firstName, string lastName, string password, string roleName, CancellationToken cancellationToken = default);
}
