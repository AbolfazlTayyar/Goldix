using Goldix.Application.Models.Group;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.Group;

public record GetAllUsersGroupQuery(int id, int page, int pageSize) : IRequest<PagedResult<UserGroupDto>>;
