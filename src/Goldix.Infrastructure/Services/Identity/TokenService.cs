using Goldix.Application.Interfaces.Identity;
using Goldix.Application.Models.Identity;
using Goldix.Domain.Entities.Identity;

namespace Goldix.Infrastructure.Services.Identity;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public GetTokenResponseDto GenerateToken(ApplicationUser user, IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expirationInMinutes = DateTime.Now.AddMinutes(_jwtOptions.ExpirationInMinutes);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: expirationInMinutes,
            signingCredentials: signinCredentials,
            claims: claims
        );

        var token = new GetTokenResponseDto
        {
            ExpirationAt = expirationInMinutes,
            UserId = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };

        return token;
    }
}
