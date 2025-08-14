using Goldix.Application.Models.Group;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.Group;

public record GetAllGroupsQuery(int page, int pageSize) : IRequest<PagedResult<GroupDto>>;
