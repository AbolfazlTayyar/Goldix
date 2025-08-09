using Goldix.Application.Models.UserRequest;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.UserRequest;

public record GetAllUserRequestsByStatusQuery(UserStatusDto dto, int page, int pageSize) : IRequest<PagedResult<UserRequestDto>>;