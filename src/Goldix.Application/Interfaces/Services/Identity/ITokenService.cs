using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Application.Models.Identity.GetToken;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface ITokenService : IScopedService
{
    GetTokenResponseDto GenerateToken(ApplicationUser user, IEnumerable<Claim> claims);
    string EncryptToken(string token);
    string DecryptToken(string encryptedToken);
}
