using Goldix.Application.Models.Notification;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.Notification;

public record GetAllNotificationsQuery(int page, int pageSize) : IRequest<PagedResult<NotificationDto>>;
