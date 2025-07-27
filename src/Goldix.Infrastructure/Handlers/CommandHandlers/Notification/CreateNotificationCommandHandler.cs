using Goldix.Application.Commands.Notification;
using Goldix.Application.Interfaces.Services.Notification;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Notification;

public class CreateNotificationCommandHandler(INotificationService notificationService) : IRequestHandler<CreateNotificationCommand>
{
    public async Task Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        await notificationService.CreateNotificationAndSendToUsersAsync(request.dto, cancellationToken);
    }
}
