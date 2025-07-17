using Goldix.Application.Models.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Identity;

public interface ITokenService
{
    GetTokenResponseDto GenerateToken(ApplicationUser user, IEnumerable<Claim> claims);
}
