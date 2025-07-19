using Goldix.API.Abstractions;
using Goldix.Application.Wrappers;

namespace Goldix.API.Endpoints.v1;

public class TokenEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var token = app.MapGroup("/api/v{version:apiVersion}/tokens")
                .WithApiVersionSet(apiVersionSet)
                .HasApiVersion(1.0);

        token.MapGet("", (IAntiforgery token, HttpContext context) =>
        {
            var generatedToken = token.GetAndStoreTokens(context);

            return ApiResponse<AntiforgeryTokenSet>.SuccessResult(generatedToken);
        });
    }
}
