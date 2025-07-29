using Goldix.Application.Interfaces.Services.Identity;

namespace Goldix.API.Middlewares;

public class TokenDecryptionMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            var encryptedToken = authHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var tokenService = context.RequestServices.GetRequiredService<ITokenService>();
                var decryptedToken = tokenService.DecryptToken(encryptedToken);

                context.Request.Headers["Authorization"] = $"Bearer {decryptedToken}";
            }
            catch (Exception)
            {
                // Token decryption failed - let authentication handler deal with it
            }
        }

        await _next(context);
    }
}
