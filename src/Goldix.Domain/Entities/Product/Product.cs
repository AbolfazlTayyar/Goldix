using Goldix.Domain.Common;

namespace Goldix.Domain.Entities.Product;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public TimeSpan? TradingStartTime { get; set; }
    public TimeSpan? TradingEndTime { get; set; }
    public bool IsActive { get; set; }
    public string Comment { get; set; }
    public int MeasurementUnitId { get; set; }

    #region Navigation Properties
    public MeasurementUnit MeasurementUnit { get; set; }
    public List<UserRequest.UserRequest> UserRequests { get; set; }
    #endregion
}
