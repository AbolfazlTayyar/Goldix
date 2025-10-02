using Goldix.Application.Extensions;
using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Handlers.QueryHandlers.UserRequest;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.Product;
using Goldix.IntegrationTests.Helpers.User;
using Goldix.IntegrationTests.Helpers.UserRequest;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.UserRequest;

public class GetUserRequestsBySearchQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetUserRequestsBySearchQueryHandler _handler;

    public GetUserRequestsBySearchQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoRequestExists_ShouldReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetTradeRequestsBySearchQuery(new TradeRequestSearchDto(), page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Fact]
    public async Task Handle_WhenNoTradeRequestExistWithConfirmedStatus_ShouldReturnsEmptyPagedResult()
    {
        // Arrange
        var sentAt = DateTime.Now;

        var users = await UserTestHelper.CreateUsersAsync(_db, 1);
        await UserRequestTestHelper.CreateUserRequestsAsync(_db, 5, users[0], RequestStatus.pending.ToDisplay(), sentAt: sentAt);

        var query = new GetTradeRequestsBySearchQuery(new TradeRequestSearchDto
        {
            StartDate = sentAt.AddDays(-5).ToShamsiDateFormatted(),
            EndDate = sentAt.AddDays(5).ToShamsiDateFormatted()
        }, page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldReturnsPagedResult()
    {
        // Arrange
        var sentAt = DateTime.Now;

        var users = await UserTestHelper.CreateUsersAsync(_db, 1);
        var products = await ProductTestHelper.CreateProductsAsync(_db, 1);

        var requests = await UserRequestTestHelper.CreateUserRequestsAsync(_db, 5, users[0], RequestStatus.confirmed.ToDisplay(), products[0], sentAt: sentAt);
        await UserRequestTestHelper.CreateUserRequestsAsync(_db, 3, users[0], RequestStatus.rejected.ToDisplay(), products[0], sentAt: sentAt.AddDays(-10));
        await UserRequestTestHelper.CreateUserRequestsAsync(_db, 2, users[0], RequestStatus.pending.ToDisplay(), products[0], sentAt: sentAt.AddDays(-3));

        var query = new GetTradeRequestsBySearchQuery(new TradeRequestSearchDto
        {
            StartDate = sentAt.AddDays(-1).ToShamsiDateFormatted(),
            EndDate = sentAt.ToShamsiDateFormatted()
        }, page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Items.Count());
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);

        var item = result.Items.First();
        Assert.Equal($"{users[0].FirstName} {users[0].LastName}", item.UserName);
        Assert.Equal(sentAt.ToShamsiDate(), item.CreatedAt);
        Assert.Equal(products[0].Name, item.ProductName);
        Assert.Equal(requests[1].ProductCount, item.ProductCount);
        Assert.Equal(requests[1].ProductPrice.ToString("N0"), item.ProductPrice);
        Assert.Equal(requests[1].ProductTotalPrice.ToString("N0"), item.ProductTotalPrice);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
