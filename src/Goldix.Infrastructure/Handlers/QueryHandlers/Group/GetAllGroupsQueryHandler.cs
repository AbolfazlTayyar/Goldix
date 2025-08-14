using Goldix.Application.Models.Group;
using Goldix.Application.Queries.Group;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Group;

public class GetAllGroupsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllGroupsQuery, PagedResult<GroupDto>>
{
    public async Task<PagedResult<GroupDto>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var count = await db.Groups
           .AsNoTracking()
           .CountAsync(cancellationToken);

        var result = await db.Groups
            .AsNoTracking()
            .ProjectTo<GroupDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<GroupDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
