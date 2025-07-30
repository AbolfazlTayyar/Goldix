using Goldix.Application.Models.Notification;
using Goldix.Application.Queries.Notification;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Notification;

public class GetAllNotificationsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllNotificationsQuery, List<NotificationDto>>
{
    public async Task<List<NotificationDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await db.NotificationContents
            .Include(x => x.Sender)
            .ToListAsync(cancellationToken);

        var result = mapper.Map<List<NotificationDto>>(notifications);

        return result;
    }
}
