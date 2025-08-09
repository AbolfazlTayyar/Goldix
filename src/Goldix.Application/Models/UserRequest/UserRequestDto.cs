namespace Goldix.Application.Models.UserRequest;

public class UserRequestDto
{
    public string UserName { get; set; }
    public string CreatedAt { get; set; }
    public string ProductName { get; set; }
    public byte ProductCount { get; set; }
    public string ProductPrice { get; set; }
    public string ProductTotalPrice { get; set; }
    public string Reason { get; set; }
}
