using Goldix.Application.Extensions;
using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Handlers.QueryHandlers.Trade;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Trade;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Trade;

public class GetTradeRequestsBySearchQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetTradeRequestsBySearchQueryHandler _handler;

    public GetTradeRequestsBySearchQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequests_ReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetTradeRequestsBySearchQuery(new TradeRequestSearchDto(), 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequestsSatisfyingCriteria_ReturnsEmptyPagedResult()
    {
        // Arrange
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 5, RequestStatus.confirmed, DateTime.Now.AddDays(-7));
        var query = new GetTradeRequestsBySearchQuery(new TradeRequestSearchDto
        {
            StartDate = DateTime.Now.AddDays(-1).Date.ToShamsiDateFormatted(),
            EndDate = DateTime.Now.AddDays(2).Date.ToShamsiDateFormatted()
        }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenTradeRequestsSatisfyingCriteriaExist_ReturnsPagedResult()
    {
        // Arrange
        var now = DateTime.Now;
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 3, RequestStatus.confirmed, now.AddDays(-1), isSenderActive: true);
        await TradeRequestTestHelper.SeedTradeRequestsAsync(_db, 2, RequestStatus.confirmed, now.AddDays(-10), isSenderActive: true);
        var query = new GetTradeRequestsBySearchQuery(new TradeRequestSearchDto
        {
            StartDate = DateTime.Now.AddDays(-1).Date.ToShamsiDateFormatted(),
            EndDate = DateTime.Now.AddDays(2).Date.ToShamsiDateFormatted()
        }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Items.Count());
        Assert.Equal(3, result.TotalCount);

        var item = result.Items.First();
        Assert.Equal("FirstName1 LastName1", item.UserName);
        Assert.Equal(now.AddDays(-1).ToShamsiDate(), item.CreatedAt);
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
