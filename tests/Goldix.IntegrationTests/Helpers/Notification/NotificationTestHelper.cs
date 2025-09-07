using Goldix.Domain.Entities.Notification;
using Goldix.Domain.Entities.User;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.Notification;

public static class NotificationTestHelper
{
    public static async Task SeedNotificationsAsync(ApplicationDbContext db, int count)
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

        db.ChangeTracker.Clear();
    }

    public static async Task<List<UserNotification>> SeedUserNotificationsAsync(ApplicationDbContext db, int count, bool isRead = false)
    {
        var userNotifications = Enumerable.Range(1, count)
            .Select(i => new UserNotification
            {
                NotificationContentId = i,
                ReceiverId = Guid.NewGuid().ToString(),
                IsRead = isRead,
            })
            .ToList();

        await db.UserNotifications.AddRangeAsync(userNotifications);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();

        return userNotifications;
    }
}
