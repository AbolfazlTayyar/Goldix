using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Domain.Entities.Common;

public abstract class BaseRequest : BaseEntity
{
    public DateTime SentAt { get; set; }
    public DateTime? RespondedAt { get; set; }
    public int? WalletTransactionId { get; set; }
    public string Status { get; set; }
    public bool? IsRead { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }

    #region Navigation Properties
    public ApplicationUser Sender { get; set; }
    public ApplicationUser Receiver { get; set; }
    public WalletTransaction WalletTransaction { get; set; }
    #endregion
}
