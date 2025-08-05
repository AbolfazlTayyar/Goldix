using Goldix.Application.Models.User;
using Goldix.Application.Queries.User;
using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.User;

public class GetAllUsersByStatusQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
    ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllUsersByStatusQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetAllUsersByStatusQuery request, CancellationToken cancellationToken)
    {
        var status = (request.dto.Status).ToDisplay();
        var userRole = await roleManager.FindByNameAsync(RoleConstants.USER);

        var result = await userManager.Users
            .AsNoTracking()
            .Where(x => x.Status == status && x.IsActive)
            .Where(x => db.UserRoles.Any(z => z.UserId == x.Id && z.RoleId == userRole.Id))
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
