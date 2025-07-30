using Goldix.Application.Models.Notification;

namespace Goldix.Application.Interfaces.Services.Notification;

public interface INotificationService
{
    Task CreateNotificationAndSendToUsersAsync(CreateNotificationDto dto, CancellationToken cancellationToken);
}
