using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Services.Identity;

public class ClaimsService(UserManager<ApplicationUser> userManager) : IClaimsService
{
    public async Task<IEnumerable<Claim>> GenerateUserClaimsAsync(ApplicationUser user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var jwtClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var allClaims = userClaims.Union(jwtClaims).Union(roleClaims);

        return allClaims;
    }
}
