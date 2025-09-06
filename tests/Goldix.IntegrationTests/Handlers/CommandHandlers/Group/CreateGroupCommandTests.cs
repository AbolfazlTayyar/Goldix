using Goldix.Application.Commands.Group;
using Goldix.Application.Models.Group;
using Goldix.Infrastructure.Handlers.CommandHandlers.Group;
using Goldix.Infrastructure.Persistence;
using Goldix.UnitTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Group;

public class CreateGroupCommandTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly CreateGroupCommandHandler _handler;

    public CreateGroupCommandTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new CreateGroupCommandHandler(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldInsertGroupInDatabase()
    {
        // Arrange
        var groupDto = new CreateUpdateGroupDto
        {
            Name = "Test Group",
            BuyPriceDifferencePercent = "50,000",
            SellPriceDifferencePercent = "30,000"
        };

        var command = new CreateGroupCommand(groupDto);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var groupInDb = await _db.Groups.FirstOrDefaultAsync(g => g.Name == "Test Group");
        Assert.NotNull(groupInDb);
        Assert.Equal("Test Group", groupInDb.Name);
        Assert.Equal("50000", groupInDb.BuyPriceDifferencePercent.ToString());
        Assert.Equal("30000", groupInDb.SellPriceDifferencePercent.ToString());
        Assert.True(groupInDb.CreatedAt <= DateTime.Now && groupInDb.CreatedAt > DateTime.Now.AddMinutes(-1));
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
