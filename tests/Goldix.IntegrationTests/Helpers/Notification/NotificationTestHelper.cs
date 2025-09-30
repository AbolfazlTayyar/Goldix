using Goldix.Domain.Entities.Notification;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.Notification;

public static class NotificationTestHelper
{
    public static async Task<List<NotificationContent>> SeedNotificationsAsync(ApplicationDbContext db, int count)
    {
        var notifications = Enumerable.Range(1, count)
            .Select(i => new NotificationContent
            {
                Id = i,
                Title = $"Notification {i}",
                Description = $"This is the description for notification {i}.",
                CreatedAt = DateTime.Now,
                Sender = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    CreatedAt = DateTime.Now,
                    PhoneNumber = $"0937214443{i}",
                    Status = "confirmed"
                }
            })
            .ToList();

        await db.NotificationContents.AddRangeAsync(notifications);
        await db.SaveChangesAsync();

        return notifications;
    }

    public static async Task<List<UserNotification>> SeedUserNotificationsAsync(ApplicationDbContext db, int count, bool isRead = false, string userId = null, NotificationContent notificationContent = null)
    {
        var userNotifications = Enumerable.Range(1, count)
            .Select(i => new UserNotification
            {
                NotificationContentId = notificationContent != null ? notificationContent.Id : i,
                ReceiverId = userId ?? Guid.NewGuid().ToString(),
                IsRead = isRead,
            })
            .ToList();

        await db.UserNotifications.AddRangeAsync(userNotifications);
        await db.SaveChangesAsync();

        return userNotifications;
    }
}
