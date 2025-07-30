using Goldix.Application.Mappings;
using Goldix.Domain.Entities.Notification;

namespace Goldix.Application.Models.Notification;

public class NotificationDto
{
    public string SenderName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string CreatedAt { get; set; }
}
