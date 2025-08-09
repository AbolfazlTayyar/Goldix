using Goldix.Domain.Entities.Notification;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Domain.Entities.User;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime CreateDate { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public string Status { get; set; }
    public int? GroupId { get; set; }
    public bool IsOnline { get; set; }

    #region Navigation Properties
    public List<NotificationContent> NotificationContents { get; set; }
    public List<UserRequest.UserRequest> UserRequests { get; set; }
    public List<WalletTransaction> WalletTransactions { get; set; }
    public Wallet Wallet { get; set; }
    #endregion
}
