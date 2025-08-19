using Goldix.Domain.Entities.Common;
using Goldix.Domain.Entities.Trade;

namespace Goldix.Domain.Entities.WalletManagement;

public class WalletTransaction : BaseEntity
{
    public int WalletId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string Reason { get; set; }
    public int? TradeRequestId { get; set; }
    public int? WalletIncreaseRequestId { get; set; }

    #region Navigation Properties
    public TradeRequest TradeRequest { get; set; }
    public WalletIncreaseRequest WalletIncreaseRequest { get; set; }
    public Wallet Wallet { get; set; }
    #endregion
}
