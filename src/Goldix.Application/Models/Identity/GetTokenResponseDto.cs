namespace Goldix.Application.Models.Identity;

public class GetTokenResponseDto
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationAt { get; set; }
}
