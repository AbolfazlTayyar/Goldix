namespace Goldix.Application.Models.Product;

public class ProductDto
{
    public string Name { get; set; }
    public string BuyPrice { get; set; }
    public string SellPrice { get; set; }
    public TimeSpan TradingStartTime { get; set; }
    public TimeSpan TradingEndTime { get; set; }
    public bool IsActive { get; set; }
    public int MeasurementUnitId { get; set; }
    public string MeasurementUnitName { get; set; }
    public string Comment { get; set; }
    public string CreateDate { get; set; }
    public string LastModifiedDate { get; set; }
}
