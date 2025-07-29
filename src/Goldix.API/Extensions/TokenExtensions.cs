using Goldix.API.Middlewares;

namespace Goldix.API.Extensions;

public static class TokenExtensions
{
    public static IApplicationBuilder UseTokenDecryption(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenDecryptionMiddleware>();
    }
}
