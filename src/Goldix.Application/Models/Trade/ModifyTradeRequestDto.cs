namespace Goldix.Application.Models.Trade;

public class ModifyTradeRequestDto : TradeRequestStatusDto
{
    public int ProductCount { get; set; }
    public string ProductPrice { get; set; }
    public string Type { get; set; }
}