using Goldix.Application.Models.User.GetToken;

namespace Goldix.Application.Commands.User;

public record AuthenticateUserCommand(GetTokenRequestDto dto) : IRequest<GetTokenResponseDto>;