using Goldix.Domain.Entities.Common;

namespace Goldix.Domain.Entities.Trade;

public class TradeRequest : BaseRequest
{
    public int ProductId { get; set; }
    public byte ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public string Type { get; set; }

    #region Navigation Properties
    public Product.Product Product { get; set; }
    #endregion
}