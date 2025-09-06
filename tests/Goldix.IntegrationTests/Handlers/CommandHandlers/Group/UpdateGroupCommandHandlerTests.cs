using Goldix.Application.Commands.Group;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Group;
using Goldix.Infrastructure.Handlers.CommandHandlers.Group;
using Goldix.Infrastructure.Persistence;
using Goldix.UnitTests.Helpers;
using Goldix.UnitTests.Helpers.User;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Group;

public class UpdateGroupCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly UpdateGroupCommandHandler _handler;

    public UpdateGroupCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new UpdateGroupCommandHandler(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldUpdateGroupInDatabase()
    {
        // Arrange
        var group = new Domain.Entities.User.Group
        {
            Name = "Original Group",
            BuyPriceDifferencePercent = 20,
            SellPriceDifferencePercent = 10,
            CreatedAt = DateTime.Now
        };
        await GroupTestHelper.SeedGroupAsync(_db, group);

        var updateDto = new CreateUpdateGroupDto
        {
            Name = "Updated Group",
            BuyPriceDifferencePercent = "40,000",
            SellPriceDifferencePercent = "25,000"
        };
        var command = new UpdateGroupCommand(group.Id, updateDto);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var groupInDb = await _db.Groups.FindAsync(group.Id);
        Assert.NotNull(groupInDb);
        Assert.Equal(1, group.Id);
        Assert.Equal("Updated Group", groupInDb.Name);
        Assert.Equal("40000", groupInDb.BuyPriceDifferencePercent.ToString());
        Assert.Equal("25000", groupInDb.SellPriceDifferencePercent.ToString());
    }

    [Fact]
    public async Task Handle_WhenGroupDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateGroupCommand(999, new CreateUpdateGroupDto());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenMultipleGroupsExist_ShouldUpdateOnlySpecifiedGroup()
    {
        // Arrange
        await GroupTestHelper.SeedGroupsRandomlyAsync(_db, 5);

        var updateDto = new CreateUpdateGroupDto
        {
            Name = "Updated Group 1",
            BuyPriceDifferencePercent = "40,000",
            SellPriceDifferencePercent = "25,000"
        };
        var command = new UpdateGroupCommand(id: 1, updateDto);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedGroup = await _db.Groups.FindAsync(1);
        var unchangedGroup = await _db.Groups.FindAsync(2);

        Assert.Equal("Updated Group 1", updatedGroup.Name);
        Assert.Equal("40000", updatedGroup.BuyPriceDifferencePercent.ToString());
        Assert.Equal("25000", updatedGroup.SellPriceDifferencePercent.ToString());
        Assert.Equal("Group 2", unchangedGroup.Name);
        Assert.Equal("20", unchangedGroup.BuyPriceDifferencePercent.ToString());
        Assert.Equal("10", unchangedGroup.SellPriceDifferencePercent.ToString());
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
