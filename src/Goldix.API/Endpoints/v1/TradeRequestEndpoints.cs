using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Trade;
using Goldix.Application.Models.Pagination;
using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class TradeRequestEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var tradeRequest = app.MapGroup("/api/v{version:apiVersion}/trade-requests")
                .WithApiVersionSet(apiVersionSet)
                .HasApiVersion(1.0);

        tradeRequest.MapGet("", async ([FromQuery] int page, [FromQuery] int pageSize, [AsParameters] TradeRequestStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetAllTradeRequestsByStatusQuery(dto, pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        tradeRequest.MapPatch("{id:int:min(1)}", async (int id, TradeRequestStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new ModifyTradeRequestCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<TradeRequestStatusDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        tradeRequest.MapGet("/search", async ([FromQuery] int page, [FromQuery] int pageSize, [AsParameters] TradeRequestSearchDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetTradeRequestsBySearchQuery(dto, pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        tradeRequest.MapGet("{userId}", async ([FromRoute] string userId, [FromQuery] int page, [FromQuery] int pageSize, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetAllUserTradeRequestsQuery(userId, pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.USER));

        tradeRequest.MapPost("", async (CreateTradeRequestDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateTradeRequestCommand(dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<CreateTradeRequestDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.USER));
    }
}
