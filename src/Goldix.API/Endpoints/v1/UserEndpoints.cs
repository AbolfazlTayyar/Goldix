using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.User;
using Goldix.Application.Models.Identity.GetToken;
using Goldix.Application.Models.Identity.Register;
using Goldix.Application.Queries.User;
using Goldix.Application.Wrappers;

namespace Goldix.API.Endpoints.v1;

public class UserEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var user = app.MapGroup("/api/v{version:apiVersion}/users")
                .WithApiVersionSet(apiVersionSet)
                .HasApiVersion(1.0);

        user.MapPost("/login", async (GetTokenRequestDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetTokenQuery(dto), cancellationToken);

            return ApiResponse<GetTokenResponseDto>.SuccessResult(result);
        }).AddEndpointFilter<ValidationFilter<GetTokenRequestDto>>();

        user.MapPost("/register", async (RegisterRequestDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new RegisterCommand(dto), cancellationToken);

            return ApiResponse<RegisterResponsetDto>.SuccessResult(result);
        }).AddEndpointFilter<ValidationFilter<RegisterRequestDto>>();
    }
}