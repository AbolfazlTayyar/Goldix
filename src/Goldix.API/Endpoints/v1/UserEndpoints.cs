using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.User;
using Goldix.Application.Extensions;
using Goldix.Application.Models.User.GetToken;
using Goldix.Application.Models.User.Register;
using Goldix.Application.Models.Notification;
using Goldix.Application.Queries.User;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;
using Goldix.Application.Models.User;
using Goldix.Application.Models.Pagination;

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

            return ApiResponse<GetTokenResponseDto>.Ok(result);
        }).AddEndpointFilter<ValidationFilter<GetTokenRequestDto>>();

        user.MapPost("/register", async (RegisterRequestDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new RegisterCommand(dto), cancellationToken);

            return ApiResponse<RegisterResponseDto>.Ok(result);
        }).AddEndpointFilter<ValidationFilter<RegisterRequestDto>>();

        user.MapGet("/notifications", async (ClaimsPrincipal user, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var userId = user.GetCurrentUserId();
            var result = await mediator.Send(new GetUserNotificationsQuery(userId), cancellationToken);

            return ApiResponse<List<NotificationDto>>.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.USER));

        user.MapGet("", async ([FromQuery] int page, [FromQuery] int pageSize, [AsParameters] UserStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            PagedRequest pagedRequest = new()
            {
                Page = page,
                PageSize = pageSize
            };
            pagedRequest.Validate();

            var result = await mediator.Send(new GetAllUsersByStatusQuery(dto, pagedRequest.Page, pagedRequest.PageSize), cancellationToken);

            return ApiResponse.Ok(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        user.MapPatch("{id}/deactivate", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new DeactivateUserCommand(id), cancellationToken);

            return ApiResponse.Ok();
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        user.MapPatch("{id}", async (string id, [AsParameters] UserStatusDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new ModifyUserStatusCommand(id, dto), cancellationToken);

            return ApiResponse.Ok();
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));
    }
}