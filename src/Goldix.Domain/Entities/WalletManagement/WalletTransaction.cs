using Goldix.Domain.Common;
using Goldix.Domain.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace Goldix.Domain.Entities.WalletManagement;

public class WalletTransaction : BaseEntity
{
    [ForeignKey("UserRequest")]
    public int? UserRequestId { get; set; }
    
    public string AdminId { get; set; }
    public int WalletId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string Reason { get; set; }

    #region Navigation Properties
    public UserRequest.UserRequest UserRequest { get; set; }
    public ApplicationUser Admin { get; set; }
    public Wallet Wallet { get; set; }
    #endregion
}
