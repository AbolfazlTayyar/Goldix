using Goldix.Application.Commands.Notification;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Notification;

public class MarkNotificationAsReadCommandHandler(ApplicationDbContext db) : IRequestHandler<MarkNotificationAsReadCommand>
{
    public async Task Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var userNotification = await db.UserNotifications.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (userNotification is null)
            throw new NotFoundException();

        if (userNotification.IsRead)
            throw new BadRequestException();

        userNotification.IsRead = true;
        userNotification.ReadedAt = DateTime.Now;
        await db.SaveChangesAsync(cancellationToken);
    }
}
