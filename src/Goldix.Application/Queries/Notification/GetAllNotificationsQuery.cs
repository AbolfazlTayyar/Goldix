using Goldix.Application.Models.Notification;

namespace Goldix.Application.Queries.Notification;

public record GetAllNotificationsQuery : IRequest<List<NotificationDto>>;
