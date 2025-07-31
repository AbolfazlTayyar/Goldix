using Goldix.Application.Interfaces.Infrastructure;
using Goldix.Application.Models.Notification;

namespace Goldix.Application.Interfaces.Services.Notification;

public interface INotificationService : IScopedService
{
    Task CreateNotificationAndSendToUsersAsync(CreateNotificationDto dto, CancellationToken cancellationToken);
}
