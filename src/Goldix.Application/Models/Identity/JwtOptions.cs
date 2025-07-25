namespace Goldix.Application.Models.Identity;

public class JwtOptions
{
    public string Key { get; set; }
    public string EncryptionKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationInMinutes { get; set; }
}
