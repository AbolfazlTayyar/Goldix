using Goldix.Application.Exceptions;
using Goldix.Application.Models.Group;
using Goldix.Application.Queries.Group;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Group;

public record GetAllUsersGroupQueryHandler(ApplicationDbContext db, RoleManager<IdentityRole> roleManager) : IRequestHandler<GetAllUsersGroupQuery, PagedResult<UserGroupDto>>
{
    public async Task<PagedResult<UserGroupDto>> Handle(GetAllUsersGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await db.Groups.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (group is null)
            throw new NotFoundException();

        var userRole = await roleManager.FindByNameAsync(RoleConstants.USER);
        if (userRole is null)
            throw new NotFoundException();

        var confirmedStatus = UserStatus.confirmed.ToDisplay();
        var baseQuery = db.Users
            .Where(x => x.IsActive && x.Status == confirmedStatus)
            .Where(x => db.UserRoles.Any(z => z.UserId == x.Id && z.RoleId == userRole.Id));

        var count = await baseQuery.CountAsync(cancellationToken);
        var result = await baseQuery
            .Select(x => new UserGroupDto
            {
                FullName = x.FirstName + " " + x.LastName,
                PhoneNumber = x.PhoneNumber,
                GroupName = x.Group != null ? x.Group.Name : "",
                IsInGroup = x.GroupId == request.id,
            })
            .OrderBy(x => x.PhoneNumber)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<UserGroupDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
