using Goldix.Application.Models.User;

namespace Goldix.Application.Queries.User;

public record GetAllUsersByStatusQuery(GetAllUsersByStatusDto dto) : IRequest<List<UserDto>>;
