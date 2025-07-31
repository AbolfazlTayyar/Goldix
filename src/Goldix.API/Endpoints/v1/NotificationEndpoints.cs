using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Notification;
using Goldix.Application.Models.Notification;
using Goldix.Application.Queries.Notification;
using Goldix.Application.Wrappers;
using Goldix.Domain.Constants;

namespace Goldix.API.Endpoints.v1;

public class NotificationEndpoints : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app, ApiVersionSet apiVersionSet)
    {
        var notification = app.MapGroup("/api/v{version:apiVersion}/notifications")
            .WithApiVersionSet(apiVersionSet)
            .HasApiVersion(1.0);

        notification.MapPost("", async (CreateNotificationDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateNotificationCommand(dto), cancellationToken);

            return ApiResponse<CreateNotificationDto>.SuccessResult();
        }).AddEndpointFilter<ValidationFilter<CreateNotificationDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        notification.MapGet("", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllNotificationsQuery(), cancellationToken);

            return ApiResponse<List<NotificationDto>>.SuccessResult(result);
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));

        notification.MapPatch("{id:int:min(1)}/read", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new MarkNotificationAsReadCommand(id), cancellationToken);

            return ApiResponse<MarkNotificationAsReadCommand>.SuccessResult();
        }).RequireAuthorization(policy => policy.RequireRole(RoleConstants.USER));
    }
}
