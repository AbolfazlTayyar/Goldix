using Goldix.Domain.Entities.Common;
using Goldix.Domain.Entities.User;

namespace Goldix.Domain.Entities.Notification;

public class NotificationContent : BaseEntity
{
    public string SenderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    #region Navigation Properties
    public ApplicationUser Sender { get; set; }
    public List<UserNotification> UserNotifications { get; set; }
    #endregion
}
