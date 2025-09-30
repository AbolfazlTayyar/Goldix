using Goldix.Application.Commands.User;
using Goldix.Application.Interfaces.Services.Identity;
using Goldix.Application.Models.User.Register;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Handlers.CommandHandlers.User;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.User;

public class RegisterCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly Mock<IUserService> _mockUserService;
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mockUserService = new Mock<IUserService>();
        _handler = new RegisterCommandHandler(_mockUserService.Object, _db);
    }

    [Fact]
    public async Task Handle_WhenRegisterUserFails_ThrowsException()
    {
        // Arrange
        var command = new RegisterCommand(new RegisterRequestDto());

        _mockUserService.Setup(us => us.RegisterUserAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<UserStatus>(),
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(new Exception());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenRegisterUserSucceeds_ReturnsUserId()
    {
        // Arrange
        var command = new RegisterCommand(new RegisterRequestDto
        {
            PhoneNumber = "1234567890",
            FirstName = "John",
            LastName = "Doe",
            Password = "Password123!"
        });

        var mockUser = new ApplicationUser
        {
            Id = "test-user-id",
            CreatedAt = DateTime.UtcNow,
            FirstName = command.dto.FirstName,
            LastName = command.dto.LastName,
            PhoneNumber = command.dto.PhoneNumber,
            UserName = command.dto.PhoneNumber,
        };

        _mockUserService.Setup(us => us.RegisterUserAsync(
            command.dto.PhoneNumber,
            command.dto.FirstName,
            command.dto.LastName,
            command.dto.Password,
            It.IsAny<string>(),
            It.IsAny<UserStatus>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(mockUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test-user-id", result.UserId);

        var wallet = await _db.Wallets.FirstOrDefaultAsync(w => w.UserId == mockUser.Id);
        Assert.NotNull(wallet);

        _mockUserService.Verify(us => us.RegisterUserAsync(
            command.dto.PhoneNumber,
            command.dto.FirstName,
            command.dto.LastName,
            command.dto.Password,
            It.IsAny<string>(),
            It.IsAny<UserStatus>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
        _mockUserService.VerifyNoOtherCalls();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
