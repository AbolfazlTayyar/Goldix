using Goldix.Application.Models.Setting;
using Goldix.Application.Queries.Setting;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Notification;

public class GetAllSettingsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllSettingsQuery, List<SettingsDto>>
{
    public async Task<List<SettingsDto>> Handle(GetAllSettingsQuery request, CancellationToken cancellationToken)
    {
        var result = await db.ApplicationSettings
            .AsNoTracking()
            .ProjectTo<SettingsDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
