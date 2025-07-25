using Goldix.Application.Models.Identity.GetToken;

namespace Goldix.Application.Queries.User;

public record GetTokenQuery(GetTokenRequestDto dto) : IRequest<GetTokenResponseDto>;