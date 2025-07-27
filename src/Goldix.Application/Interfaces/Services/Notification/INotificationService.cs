using Goldix.Application.Models.Notification;

namespace Goldix.Application.Interfaces.Services.Notification;

public interface INotificationService
{
    Task CreateNotificationAndSendToUsersAsync(NotificationContentDto dto, CancellationToken cancellationToken);
}
