using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.Identity.GetToken;
using Goldix.Application.Queries.User;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.User;

public class GetTokenQueryHandler(IAuthenticationService authenticationService, IClaimsService claimsService, ITokenService tokenService) 
    : IRequestHandler<GetTokenQuery, GetTokenResponseDto>
{
    public async Task<GetTokenResponseDto> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.ValidateUserAsync(request.dto.UserName, request.dto.Password, cancellationToken);
        var claims = await claimsService.GenerateUserClaimsAsync(user);
        var tokenGenerationResponse = tokenService.GenerateToken(user, claims);
        var encryptedToken = tokenService.EncryptToken(tokenGenerationResponse?.Token);

        GetTokenResponseDto result = new()
        {
            Token = encryptedToken,
            ExpirationAt = tokenGenerationResponse.ExpirationAt,
            UserId = tokenGenerationResponse.UserId
        };

        return result;
    }
}
