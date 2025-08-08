using Goldix.Application.Models.User;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.User;

public record GetAllUsersByStatusQuery(GetAllUsersByStatusDto dto, int page, int pageSize) : IRequest<PagedResult<UserDto>>;
