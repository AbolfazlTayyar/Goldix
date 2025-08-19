using Goldix.Application.Models.Notification;
using Goldix.Application.Queries.User;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.User;

public class GetUserNotificationsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetUserNotificationsQuery, List<NotificationDto>>
{
    public async Task<List<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var result = await db.UserNotifications
            .AsNoTracking()
            .Where(x => x.ReceiverId == request.currentUserId)
            .Include(x => x.NotificationContent)
            .ProjectTo<NotificationDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
