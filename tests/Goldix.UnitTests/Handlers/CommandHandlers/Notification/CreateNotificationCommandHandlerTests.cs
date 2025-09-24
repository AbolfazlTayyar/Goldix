using Goldix.Application.Commands.Notification;
using Goldix.Application.Interfaces.Services.Notification;
using Goldix.Application.Models.Notification;
using Goldix.Infrastructure.Handlers.CommandHandlers.Notification;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Notification;

public class CreateNotificationCommandHandlerTests
{
    private readonly Mock<INotificationService> _notificationServiceMock;
    private readonly CreateNotificationCommandHandler _handler;

    public CreateNotificationCommandHandlerTests()
    {
        _notificationServiceMock = new Mock<INotificationService>();
        _handler = new CreateNotificationCommandHandler(_notificationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Call_CreateNotificationAndSendToUsersAsync()
    {
        // Arrange
        var command = new CreateNotificationCommand(new CreateNotificationDto());

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _notificationServiceMock.Verify(service => service.CreateNotificationAndSendToUsersAsync(command.dto, It.IsAny<CancellationToken>()), Times.Once);
    }
}
