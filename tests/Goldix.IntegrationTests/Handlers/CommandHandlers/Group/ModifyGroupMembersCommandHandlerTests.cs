using Goldix.Application.Commands.Group;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Group;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Handlers.CommandHandlers.Group;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Group;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.User;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Group;

public class ModifyGroupMembersCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly ModifyGroupMembersCommandHandler _handler;

    public ModifyGroupMembersCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new ModifyGroupMembersCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenNoPhoneNumbersProvided_ShouldThrowBadRequestException()
    {
        // Arrange
        var command = new ModifyGroupMembersCommand(1, new ModifyGroupMembersDto
        {
            UsersToAdd = new List<string>(),
            UsersToRemove = new List<string>()
        });

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenGroupDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = GroupTestHelper.BuildModifyGroupMembersCommand();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenNoUsersFound_ShouldThrowNotFoundException()
    {
        // Arrange
        await GroupTestHelper.SeedGroupsAsync(_db, 1);

        var command = GroupTestHelper.BuildModifyGroupMembersCommand();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldAddOrRemoveUsersFromGroup()
    {
        // Arrange
        await GroupTestHelper.SeedGroupsAsync(_db, 5);
        await UserTestHelper.SeedUsersAsync(_db, 5, UserStatus.confirmed.ToDisplay(), true);

        var command = new ModifyGroupMembersCommand(3, new ModifyGroupMembersDto
        {
            UsersToAdd = new List<string> { "09000000002", "09000000009" },
            UsersToRemove = new List<string> { "09000000001", "09000000004" }
        });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var userToAdd = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == "09000000002");
        var userToRemove = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == "09000000004");
        Assert.NotNull(userToAdd);
        Assert.Equal(3, userToAdd.GroupId);
        Assert.NotNull(userToRemove);
        Assert.Null(userToRemove.GroupId);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
