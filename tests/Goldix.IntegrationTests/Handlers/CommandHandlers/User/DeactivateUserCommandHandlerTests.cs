using Goldix.Application.Commands.User;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Handlers.CommandHandlers.User;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.User;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.User;

public class DeactivateUserCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly DeactivateUserCommandHandler _handler;

    public DeactivateUserCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new DeactivateUserCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeactivateUserCommand(Guid.NewGuid().ToString());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldDeactivateUser()
    {
        // Arrange
        var users = await UserTestHelper.CreateUsersAsync(_db, 1);
        var userId = users.First().Id;
        var command = new DeactivateUserCommand(userId);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        Assert.NotNull(updatedUser);
        Assert.False(updatedUser.IsActive);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
