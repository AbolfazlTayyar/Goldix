using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IAuthenticationService : IScopedService
{
    Task<ApplicationUser> ValidateUserAsync(string username, string password, CancellationToken cancellationToken); 
}
