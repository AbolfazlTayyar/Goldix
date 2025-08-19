using Goldix.Domain.Entities.Notification;
using Goldix.Domain.Entities.Trade;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Domain.Entities.User;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public string Status { get; set; }
    public int? GroupId { get; set; }
    public bool IsOnline { get; set; }

    #region Navigation Properties
    public List<NotificationContent> NotificationContents { get; set; }
    public List<UserNotification> UserNotifications { get; set; }
    public Wallet Wallet { get; set; }
    public Group Group { get; set; }

    public List<WalletIncreaseRequest> SentWalletIncreaseRequests { get; set; }
    public List<WalletIncreaseRequest> ReceivedWalletIncreaseRequests { get; set; }

    public List<TradeRequest> SentTradeRequests { get; set; }
    public List<TradeRequest> ReceivedTradeRequests { get; set; }
    #endregion
}
