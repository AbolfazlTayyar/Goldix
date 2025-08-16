using Goldix.Application.Interfaces.Services.Notification;
using Goldix.Application.Models.Notification;
using Goldix.Domain.Constants;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.Notification;
using Goldix.Infrastructure.Persistence;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;

namespace Goldix.Infrastructure.Services.Notification;

public class NotificationService(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager) : INotificationService
{
    public async Task CreateNotificationAndSendToUsersAsync(CreateNotificationDto dto, CancellationToken cancellationToken)
    {
        using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var notification = mapper.Map<NotificationContent>(dto);
        notification.CreatedAt = DateTime.Now;

        await db.NotificationContents.AddAsync(notification, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);

        var users = await userManager.GetUsersInRoleAsync(RoleConstants.USER);
        if (users.Count == 0)
            throw new InvalidOperationException("No users found in USER role");

        var confirmedStatus = UserStatus.confirmed.ToDisplay();
        var notifications = users.Where(x => x.Status == confirmedStatus)
            .Select(user => new UserNotification
            {
                NotificationContentId = notification.Id,
                IsRead = false,
                UserId = user.Id,
            })
            .ToList();

        await db.UserNotifications.AddRangeAsync(notifications, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
