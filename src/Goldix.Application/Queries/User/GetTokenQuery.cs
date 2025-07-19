using Goldix.Application.Models.Identity;

namespace Goldix.Application.Queries.User;

public record GetTokenQuery(GetTokenRequestDto dto) : IRequest<GetTokenResponseDto>;