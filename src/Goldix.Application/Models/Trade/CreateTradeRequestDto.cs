namespace Goldix.Application.Models.Trade;

public class CreateTradeRequestDto
{
    public int ProductId { get; set; }
    public byte ProductCount { get; set; }
    public string ProductPrice { get; set; }
    public string Type { get; set; }
}
