using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Product;
using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
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

        measurementUnit.MapGet("", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllMeasurementUnitsQuery(), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        measurementUnit.MapPost("", async (MeasurementUnitDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateMeasurementUnitCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<MeasurementUnitDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        measurementUnit.MapPut("{id:int:min(1)}", async (MeasurementUnitDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new UpdateMeasurementUnitCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<MeasurementUnitDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        measurementUnit.MapDelete("{id:int:min(1)}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new DeleteMeasurementUnitCommand(id), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<MeasurementUnitDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));
    }
}
