using Goldix.Application.Commands.User;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.User.GetToken;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.User;

public class AuthenticateUserCommandHandler(IAuthenticationService authenticationService, IClaimsService claimsService, ITokenService tokenService)
    : IRequestHandler<AuthenticateUserCommand, GetTokenResponseDto>
{
    public async Task<GetTokenResponseDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
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
