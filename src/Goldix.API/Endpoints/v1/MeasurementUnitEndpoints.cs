using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Product;
using Goldix.Application.Models.Product;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class MeasurementUnitEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var measurementUnit = app.MapGroup("/api/v{version:apiVersion}/measurement-units")
            .WithApiVersionSet(apiVersionSet)
            .HasApiVersion(1.0);

        measurementUnit.MapPost("", async (CreateMeasurementUnitDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateMeasurementUnitCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<CreateMeasurementUnitDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));
    }
}
