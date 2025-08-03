using Goldix.Application.Models.User;
using Goldix.Application.Queries.User;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.User;

public class GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var status = UserStatus.confirmed.ToDisplay();

        var result = await userManager.Users
            .AsNoTracking()
            .Where(x => x.Status == status && x.IsActive)
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;  
    }
}
