using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IAuthenticationService
{
    Task<ApplicationUser> ValidateUserAsync(string username, string password, CancellationToken cancellationToken); 
}
