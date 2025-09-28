using Goldix.Application.Models.User;
using Goldix.Application.Queries.User;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.User;

public class GetAllUsersByStatusQueryHandler(RoleManager<IdentityRole> roleManager,
    ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllUsersByStatusQuery, PagedResult<UserDto>>
{
    public async Task<PagedResult<UserDto>> Handle(GetAllUsersByStatusQuery request, CancellationToken cancellationToken)
    {
        var status = (request.dto.Status).ToDisplay();
        var userRole = await roleManager.FindByNameAsync(RoleConstants.USER);

        var baseQuery = db.Users
            .AsNoTracking()
            .Where(x => x.Status == status && x.IsActive)
            .Where(x => db.UserRoles.Any(z => z.UserId == x.Id && z.RoleId == userRole.Id));

        var count = await baseQuery.CountAsync(cancellationToken);

        var result = await baseQuery
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<UserDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}