using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface IClaimsService : IScopedService
{
    Task<IEnumerable<Claim>> GenerateUserClaimsAsync(ApplicationUser user);
}
