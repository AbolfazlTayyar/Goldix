using Goldix.Domain.Entities.Common;
using Goldix.Domain.Entities.User;

namespace Goldix.Domain.Entities.Notification;

public class UserNotification : BaseEntity
{
    public int NotificationContentId { get; set; }
    public string ReceiverId { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadedAt { get; set; }

    #region Navigation Properties
    public NotificationContent NotificationContent { get; set; }
    public ApplicationUser Receiver { get; set; }
    #endregion
}
