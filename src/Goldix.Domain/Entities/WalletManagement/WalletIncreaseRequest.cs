using Goldix.Domain.Entities.Common;

namespace Goldix.Domain.Entities.WalletManagement;

public class WalletIncreaseRequest : BaseRequest
{
    public decimal Amount { get; set; }
}
