using Goldix.Application.Mappings;
using Goldix.Domain.Entities.Notification;

namespace Goldix.Application.Models.Notification;

public class NotificationContentDto : IMapFrom<NotificationContent>
{
    public string SenderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
