using Goldix.Application.Commands.Notification;
using Goldix.Application.Models.Notification;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Handlers.CommandHandlers.Notification;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.Infrastructure.Services.Notification;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Notification;

public class CreateNotificationCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly NotificationService _notificationService;
    private readonly CreateNotificationCommandHandler _handler;

    public CreateNotificationCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _mockUserManager = BaseHelper.MockUserManager();
        _notificationService = new NotificationService(_db, _mapper, _mockUserManager.Object);
        _handler = new CreateNotificationCommandHandler(_notificationService);
    }

    [Fact]
    public async Task Handle_WhenNoUserInRole_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var command = new CreateNotificationCommand
        (
            new CreateNotificationDto
            {
                SenderId = "admin-id",
                Title = "Test Notification",
                Description = "This is a test notification."
            }
        );

        _mockUserManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<ApplicationUser>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenNoConfirmedUser_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var command = new CreateNotificationCommand
        (
            new CreateNotificationDto
            {
                SenderId = "admin-id",
                Title = "Test Notification",
                Description = "This is a test notification."
            }
        );

        var users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = "user1",
                FirstName = "John",
                LastName = "Doe",
                CreatedAt = DateTime.Now,
                UserName = "user1",
                Status = UserStatus.rejected.ToDisplay()
            },
            new ApplicationUser
            {
                Id = "user2",
                FirstName = "Jane",
                LastName = "Smith",
                CreatedAt = DateTime.Now,
                UserName = "user2",
                Status = UserStatus.waiting.ToDisplay()
            }
        };
        _mockUserManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>()))
            .ReturnsAsync(users);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldCreateNotificationAndUserNotifications()
    {
        // Arrange
        var command = new CreateNotificationCommand
        (
            new CreateNotificationDto
            {
                SenderId = "admin-id",
                Title = "Test Notification",
                Description = "This is a test notification."
            }
        );

        var users = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = "user1",
                FirstName = "John",
                LastName = "Doe",
                CreatedAt = DateTime.Now,
                UserName = "user1",
                Status = UserStatus.confirmed.ToDisplay()
            },
            new ApplicationUser
            {
                Id = "user2",
                FirstName = "Jane",
                LastName = "Smith",
                CreatedAt = DateTime.Now,
                UserName = "user2",
                Status = UserStatus.confirmed.ToDisplay()
            }
        };
        _mockUserManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>()))
            .ReturnsAsync(users);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var notificationContent = await _db.NotificationContents.FirstOrDefaultAsync();
        Assert.NotNull(notificationContent);
        Assert.Equal(command.dto.Title, notificationContent.Title);
        Assert.Equal(command.dto.Description, notificationContent.Description);
        Assert.Equal(command.dto.SenderId, notificationContent.SenderId);
        var userNotifications = await _db.UserNotifications.ToListAsync();
        Assert.Equal(2, userNotifications.Count);
        Assert.All(userNotifications, un => Assert.False(un.IsRead));
        Assert.Contains(userNotifications, un => un.ReceiverId == "user1");
        Assert.Contains(userNotifications, un => un.ReceiverId == "user2");
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
