using Goldix.Application.Models.Notification;

namespace Goldix.Application.Commands.Notification;

public record CreateNotificationCommand(CreateNotificationDto dto) : IRequest;
