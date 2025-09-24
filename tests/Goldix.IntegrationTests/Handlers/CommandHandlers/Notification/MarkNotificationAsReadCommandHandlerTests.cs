using Goldix.Application.Commands.Notification;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Handlers.CommandHandlers.Notification;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Notification;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Notification;

public class MarkNotificationAsReadCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly MarkNotificationAsReadCommandHandler _handler;

    public MarkNotificationAsReadCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new MarkNotificationAsReadCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenNotificationDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new MarkNotificationAsReadCommand(999);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenNotificationIdIsInvalid_ShouldThrowNotFoundException()
    {
        // Arrange
        await NotificationTestHelper.SeedUserNotificationsAsync(_db, 5);

        var command = new MarkNotificationAsReadCommand(7);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenNotificationAlreadyRead_ShouldNotChangeReadStatus()
    {
        // Arrange
        var userNotifications = await NotificationTestHelper.SeedUserNotificationsAsync(_db, 5, isRead: true);

        var command = new MarkNotificationAsReadCommand(1);

        // Act
        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldMarkNotificationAsReadInDatabase()
    {
        // Arrange
        await NotificationTestHelper.SeedUserNotificationsAsync(_db, 5);

        var command = new MarkNotificationAsReadCommand(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var notificationInDb = await _db.UserNotifications.FindAsync(1);
        Assert.NotNull(notificationInDb);
        Assert.True(notificationInDb.IsRead);
        Assert.NotNull(notificationInDb.ReadedAt);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
