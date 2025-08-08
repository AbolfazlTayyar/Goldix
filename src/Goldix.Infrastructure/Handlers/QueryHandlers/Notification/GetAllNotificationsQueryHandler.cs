using Goldix.Application.Models.Notification;
using Goldix.Application.Queries.Notification;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Notification;

public class GetAllNotificationsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllNotificationsQuery, PagedResult<NotificationDto>>
{
    public async Task<PagedResult<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var count = await db.NotificationContents
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var result = await db.NotificationContents
            .AsNoTracking()
            .Include(x => x.Sender)
            .ProjectTo<NotificationDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<NotificationDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
