using Goldix.Application.Mappings.Common;
using Goldix.Domain.Entities.Notification;

namespace Goldix.Application.Models.Notification;

public class CreateNotificationDto : IMapFrom<NotificationContent>
{
    public string SenderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
