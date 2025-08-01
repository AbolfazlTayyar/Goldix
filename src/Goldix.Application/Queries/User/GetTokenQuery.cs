using Goldix.Application.Models.User.GetToken;

namespace Goldix.Application.Queries.User;

public record GetTokenQuery(GetTokenRequestDto dto) : IRequest<GetTokenResponseDto>;