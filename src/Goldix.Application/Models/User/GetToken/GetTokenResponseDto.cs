namespace Goldix.Application.Models.User.GetToken;

public class GetTokenResponseDto
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationAt { get; set; }
}
