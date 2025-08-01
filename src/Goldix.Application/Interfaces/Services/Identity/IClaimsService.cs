using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Domain.Entities.User;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IClaimsService : IScopedService
{
    Task<IEnumerable<Claim>> GenerateUserClaimsAsync(ApplicationUser user);
}
