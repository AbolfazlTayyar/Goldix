using Goldix.Domain.Common;
using Goldix.Domain.Entities.User;

namespace Goldix.Domain.Entities.WalletManagement;

public class Wallet : BaseEntity
{
    public string UserId { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    #region Navigation Properties
    public ApplicationUser User { get; set; }
    public List<WalletTransaction> WalletTransactions { get; set; }
    #endregion
}
