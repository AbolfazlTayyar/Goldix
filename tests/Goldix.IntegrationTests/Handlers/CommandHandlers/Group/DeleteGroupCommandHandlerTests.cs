using Goldix.Application.Commands.Group;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Handlers.CommandHandlers.Group;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Group;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Group;

public class DeleteGroupCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly DeleteGroupCommandHandler _handler;

    public DeleteGroupCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new DeleteGroupCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldDeleteGroupFromDatabase()
    {
        // Arrange
        var group = new Domain.Entities.User.Group
        {
            Name = "Group to Delete",
            BuyPriceDifferencePercent = 20,
            SellPriceDifferencePercent = 10,
            CreatedAt = DateTime.Now
        };

        await GroupTestHelper.SeedGroupAsync(_db, group);

        var command = new DeleteGroupCommand(group.Id);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var groupInDb = await _db.Groups.FindAsync(group.Id);
        Assert.Null(groupInDb);
    }

    [Fact]
    public async Task Handle_WhenGroupDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeleteGroupCommand(999);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
