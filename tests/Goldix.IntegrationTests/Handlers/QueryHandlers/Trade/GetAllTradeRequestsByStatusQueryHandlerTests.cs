using Goldix.Application.Extensions;
using Goldix.Application.Queries.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Handlers.QueryHandlers.Trade;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Trade;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Trade;

public class GetAllTradeRequestsByStatusQueryHandlerTests
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllTradeRequestsByStatusQueryHandler _handler;

    public GetAllTradeRequestsByStatusQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequests_ReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllTradeRequestsByStatusQuery(new() { Status = RequestStatus.pending }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequestsWithGivenStatus_ReturnsEmptyPagedResult()
    {
        // Arrange
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.confirmed, DateTime.Now);

        var query = new GetAllTradeRequestsByStatusQuery(new() { Status = RequestStatus.pending }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenNoActiveUser_ReturnEmptyPagedResult()
    {
        // Arrange
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.pending, DateTime.Now, isSenderActive: false);

        var query = new GetAllTradeRequestsByStatusQuery(new() { Status = RequestStatus.pending }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenTradeRequestsExist_ReturnsPagedResult()
    {
        // Arrange
        DateTime now = DateTime.Now;
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.pending, now);
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.confirmed, now);
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.rejected, now);

        var query = new GetAllTradeRequestsByStatusQuery(new() { Status = RequestStatus.pending }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(5, result.Items.Count());

        var item = result.Items.First();
        Assert.Equal("FirstName1 LastName1", item.UserName);
        Assert.Equal(now.ToShamsiDate(), item.CreatedAt);
        Assert.Equal("Product 1", item.ProductName);
        Assert.Equal(1, item.ProductCount);
        Assert.Equal("10,001", item.ProductPrice);
        Assert.Equal("10,001", item.ProductTotalPrice);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
