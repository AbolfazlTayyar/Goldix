using Goldix.Application.Commands.User;
using Goldix.Application.Models.User;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Handlers.CommandHandlers.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.User;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.User;

public class ModifyUserStatusCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly ModifyUserStatusCommandHandler _handler;

    public ModifyUserStatusCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new ModifyUserStatusCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new ModifyUserStatusCommand("", new UserStatusDto());

        // Act & Assert
        await Assert.ThrowsAsync<Goldix.Application.Exceptions.NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenUserExists_ShouldModifyStatus()
    {
        // Arrange
        var users = await UserTestHelper.CreateUsersAsync(_db, 1);

        var command = new ModifyUserStatusCommand(users[0].Id, new UserStatusDto
        {
            Status = UserStatus.rejected,
        });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedUser = await _db.Users.FindAsync(users[0].Id);
        Assert.NotNull(updatedUser);
        Assert.Equal(UserStatus.rejected.ToDisplay(), updatedUser.Status);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
