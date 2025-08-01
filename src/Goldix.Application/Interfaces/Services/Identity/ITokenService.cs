using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Application.Models.User.GetToken;
using Goldix.Domain.Entities.User;

namespace Goldix.Application.Interfaces.Services.Identity;

public interface ITokenService : IScopedService
{
    GetTokenResponseDto GenerateToken(ApplicationUser user, IEnumerable<Claim> claims);
    string EncryptToken(string token);
    string DecryptToken(string encryptedToken);
}
