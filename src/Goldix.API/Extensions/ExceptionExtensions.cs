using Goldix.API.Middlewares;

namespace Goldix.API.Extensions;

public static class ExceptionExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
