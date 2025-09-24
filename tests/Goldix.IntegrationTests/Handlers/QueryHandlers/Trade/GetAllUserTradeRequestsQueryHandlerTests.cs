using Goldix.Application.Queries.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Handlers.QueryHandlers.Trade;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Trade;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Trade;

public class GetAllUserTradeRequestsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllUserTradeRequestsQueryHandler _handler;

    public GetAllUserTradeRequestsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequests_ReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllUserTradeRequestsQuery(Guid.NewGuid().ToString(), 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequestsForGivenUserId_ReturnsEmptyPagedResult()
    {
        // Arrange
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 1, RequestStatus.confirmed, DateTime.Now);

        var query = new GetAllUserTradeRequestsQuery(Guid.NewGuid().ToString(), 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenTradeRequestsForGivenUserIdExist_ReturnsPagedResult()
    {
        // Arrange
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.pending, DateTime.Now, userId: "Id 1");

        var query = new GetAllUserTradeRequestsQuery("Id 1", 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(5, result.Items.Count());
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
