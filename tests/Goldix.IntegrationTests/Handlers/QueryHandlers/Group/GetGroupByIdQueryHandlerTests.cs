using Goldix.Application.Exceptions;
using Goldix.Application.Extensions;
using Goldix.Application.Queries.Group;
using Goldix.Infrastructure.Handlers.QueryHandlers.Group;
using Goldix.Infrastructure.Persistence;
using Goldix.UnitTests.Helpers;
using Goldix.UnitTests.Helpers.User;

namespace Goldix.UnitTests.Handlers.QueryHandlers.Group;

public class GetGroupByIdQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetGroupByIdQueryHandler _handler;

    public GetGroupByIdQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new GetGroupByIdQueryHandler(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenGroupExists_ShouldReturnMappedGroupDto()
    {
        // Arrange
        var groupId = 1;
        var groupName = "Test Group";
        var createdAt = new DateTime(2024, 1, 15, 10, 30, 0);
        var buyPriceDifferencePercent = 10;
        var sellPriceDifferencePercent = 7;

        var group = new Domain.Entities.User.Group
        {
            Id = groupId,
            Name = groupName,
            CreatedAt = createdAt,
            BuyPriceDifferencePercent = buyPriceDifferencePercent,
            SellPriceDifferencePercent = sellPriceDifferencePercent
        };

        await GroupTestHelper.SeedGroupAsync(_db, group);
        var query = new GetGroupByIdQuery(id: 1);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(groupId, result.Id);
        Assert.Equal(groupName, result.Name);
        Assert.Equal(createdAt.ToShamsiDate(), result.CreatedAt);
        Assert.Equal(buyPriceDifferencePercent.ToString(), result.BuyPriceDifferencePercent);
        Assert.Equal(sellPriceDifferencePercent.ToString(), result.SellPriceDifferencePercent);
    }

    [Fact]
    public async Task Handle_WhenGroupDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var query = new GetGroupByIdQuery(id: 999);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
