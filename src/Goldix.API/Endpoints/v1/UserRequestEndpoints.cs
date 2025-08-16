using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.UserRequest;
using Goldix.Application.Models.Pagination;
using Goldix.Application.Models.UserRequest;
using Goldix.Application.Queries.UserRequest;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class UserRequestEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var userRequest = app.MapGroup("/api/v{version:apiVersion}/user-requests")
                .WithApiVersionSet(apiVersionSet)
                .HasApiVersion(1.0)
                .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        userRequest.MapGet("", async ([FromQuery] int page, [FromQuery] int pageSize, [AsParameters] UserStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetAllUserRequestsByStatusQuery(dto, pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        });

        userRequest.MapPatch("{id:int:min(1)}", async (int id, UserStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new ModifyUserRequestCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).AddEndpointFilter<ValidationFilter<UserStatusDto>>();

        userRequest.MapGet("/search", async ([FromQuery] int page, [FromQuery] int pageSize, [AsParameters] UserRequestSearchDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetUserRequestsBySearchQuery(dto, pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        });
    }
}
