using Goldix.Domain.Entities.Common;
using Goldix.Domain.Entities.Trade;

namespace Goldix.Domain.Entities.Product;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public TimeSpan? TradingStartTime { get; set; }
    public TimeSpan? TradingEndTime { get; set; }
    public bool IsActive { get; set; }
    public string Comment { get; set; }
    public int MeasurementUnitId { get; set; }

    #region Navigation Properties
    public MeasurementUnit MeasurementUnit { get; set; }
    public List<TradeRequest> TradeRequests { get; set; }
    public List<Asset> Assets { get; set; }
    #endregion
}
