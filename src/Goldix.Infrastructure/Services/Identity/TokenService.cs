using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.User;
using Goldix.Application.Models.User.GetToken;
using Goldix.Domain.Entities.User;
using System.Security.Cryptography;

namespace Goldix.Infrastructure.Services.Identity;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public GetTokenResponseDto GenerateToken(ApplicationUser user, IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expirationInMinutes = DateTime.Now.AddMinutes(_jwtOptions.ExpirationInMinutes);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: expirationInMinutes,
            signingCredentials: signinCredentials,
            claims: claims
        );

        var token = new GetTokenResponseDto
        {
            ExpirationAt = expirationInMinutes,
            UserId = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };

        return token;
    }

    public string EncryptToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Token cannot be null or empty");

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_jwtOptions.EncryptionKey);
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var encryptedBytes = encryptor.TransformFinalBlock(tokenBytes, 0, tokenBytes.Length);

        var result = new byte[aes.IV.Length + encryptedBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
        Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

        return Convert.ToBase64String(result);
    }

    public string DecryptToken(string encryptedToken)
    {
        if (string.IsNullOrEmpty(encryptedToken))
            throw new ArgumentException("Encrypted token cannot be null or empty");

        var fullCipher = Convert.FromBase64String(encryptedToken);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_jwtOptions.EncryptionKey);

        // Extract IV (first 16 bytes)
        var iv = new byte[16];
        var encryptedBytes = new byte[fullCipher.Length - 16];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, 16);
        Buffer.BlockCopy(fullCipher, 16, encryptedBytes, 0, encryptedBytes.Length);

        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
