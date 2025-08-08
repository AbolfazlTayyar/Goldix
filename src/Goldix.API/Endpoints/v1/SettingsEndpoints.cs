using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Setting;
using Goldix.Application.Models.Setting;
using Goldix.Application.Queries.Setting;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class SettingsEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var user = app.MapGroup("/api/v{version:apiVersion}/settings")
                .WithApiVersionSet(apiVersionSet)
                .HasApiVersion(1.0)
                .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        user.MapPost("", async (CreateUpdateSettingsDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateSettingsCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<CreateUpdateSettingsDto>>();

        user.MapPut("", async (CreateUpdateSettingsDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateSettingsCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<CreateUpdateSettingsDto>>();

        user.MapGet("", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllSettingsQuery(), cancellationToken);

            return ApiResponse<List<SettingsDto>>.Ok(result);
        });
    }
}