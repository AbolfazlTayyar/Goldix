using Goldix.Domain.Common;

namespace Goldix.Domain.Entities.Notification;

public class UserNotification : BaseEntity
{
    public int NotificationContentId { get; set; }
    public string UserId { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadedAt { get; set; }

    #region Navigation Properties
    public NotificationContent NotificationContent { get; set; }
    #endregion
}
