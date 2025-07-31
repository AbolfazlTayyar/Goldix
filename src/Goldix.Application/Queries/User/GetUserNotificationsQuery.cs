using Goldix.Application.Models.Notification;

namespace Goldix.Application.Queries.User;

public record GetUserNotificationsQuery(string currentUserId) : IRequest<List<NotificationDto>>;
