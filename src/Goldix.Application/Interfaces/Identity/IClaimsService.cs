using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Identity;

public interface IClaimsService
{
    Task<IEnumerable<Claim>> GenerateUserClaimsAsync(ApplicationUser user);
}
