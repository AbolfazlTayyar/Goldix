using Goldix.Domain.Common;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Domain.Entities.User;

public class UserRequest : BaseEntity
{
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProductId { get; set; }
    public byte ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public string Status { get; set; }

    #region Navigation Properties
    public ApplicationUser User { get; set; }
    public WalletTransaction WalletTransaction { get; set; }
    #endregion
}