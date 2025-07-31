namespace Goldix.Application.Commands.Notification;

public record MarkNotificationAsReadCommand(int id) : IRequest;
