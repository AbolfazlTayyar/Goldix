using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Domain.Entities.User;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IUserService : IScopedService
{
    Task<ApplicationUser> RegisterUserAsync(string phoneNumber, string firstName, string lastName, string password, string roleName, CancellationToken cancellationToken = default);
}
