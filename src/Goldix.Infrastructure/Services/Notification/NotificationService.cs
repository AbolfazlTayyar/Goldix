using Goldix.Application.Interfaces.Services.Notification;
using Goldix.Application.Models.Notification;
using Goldix.Domain.Constants;
using Goldix.Domain.Entities.Notification;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Services.Notification;

public class NotificationService(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager) : INotificationService
{
    public async Task CreateNotificationAndSendToUsersAsync(CreateNotificationDto dto, CancellationToken cancellationToken)
    {
        if (db.Database.IsInMemory()) // for testing purposes   
        {
            await Operation(dto, cancellationToken);
        }
        else
        {
            using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
            await Operation(dto, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
    }

    private async Task Operation(CreateNotificationDto dto, CancellationToken cancellationToken)
    {
        var notification = mapper.Map<NotificationContent>(dto);
        notification.CreatedAt = DateTime.Now;

        await db.NotificationContents.AddAsync(notification, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        var users = await userManager.GetUsersInRoleAsync(RoleConstants.USER);
        if (users.Count == 0)
            throw new InvalidOperationException();

        var confirmedStatus = UserStatus.confirmed.ToDisplay();
        var notifications = users.Where(x => x.Status == confirmedStatus)
            .Select(user => new UserNotification
            {
                NotificationContentId = notification.Id,
                IsRead = false,
                ReceiverId = user.Id,
            })
            .ToList();
        if (notifications.Count == 0)
            throw new InvalidOperationException();

        await db.UserNotifications.AddRangeAsync(notifications, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);
    }
}
