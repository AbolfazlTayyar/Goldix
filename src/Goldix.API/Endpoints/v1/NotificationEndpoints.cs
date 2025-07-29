using Goldix.API.Abstractions;
using Goldix.API.Filters;
using Goldix.Application.Commands.Notification;
using Goldix.Application.Models.Notification;
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

        notification.MapPost("", async (NotificationContentDto dto, IMediator mediator, CancellationToken cancellationToken) =>
        {
            await mediator.Send(new CreateNotificationCommand(dto), cancellationToken);

            return ApiResponse<NotificationContentDto>.SuccessResult();
        }).AddEndpointFilter<ValidationFilter<NotificationContentDto>>()
          .RequireAuthorization(policy => policy.RequireRole(RoleConstants.ADMIN));
    }
}
