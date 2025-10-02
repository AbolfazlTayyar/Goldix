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

public class GetAllRequestsByStatusQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllRequestsByStatusQueryHandler _handler;

    public GetAllRequestsByStatusQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoRequest_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllTradeRequestsByStatusQuery(new TradeRequestStatusDto(), 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenNoRequestWithGivenStatus_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        var users = await UserTestHelper.CreateUsersAsync(_db, 1);
        await UserRequestTestHelper.CreateUserRequestsAsync(_db, 1, users[0], RequestStatus.pending.ToDisplay());

        var query = new GetAllTradeRequestsByStatusQuery(new TradeRequestStatusDto { Status = RequestStatus.rejected }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenSenderIsNotActive_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        var users = await UserTestHelper.CreateUsersAsync(_db, 1, isActive: false);
        await UserRequestTestHelper.CreateUserRequestsAsync(_db, 1, users[0], RequestStatus.pending.ToDisplay());

        var query = new GetAllTradeRequestsByStatusQuery(new TradeRequestStatusDto { Status = RequestStatus.pending }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldReturnPagedResult()
    {
        // Arrange
        var sentAt = DateTime.Now;

        var users = await UserTestHelper.CreateUsersAsync(_db, 1);
        var products = await ProductTestHelper.CreateProductsAsync(_db, 1);
        var requests = await UserRequestTestHelper.CreateUserRequestsAsync(_db, 15, users[0], RequestStatus.pending.ToDisplay(), product: products[0], sentAt: sentAt);

        var query = new GetAllTradeRequestsByStatusQuery(new TradeRequestStatusDto { Status = RequestStatus.pending }, 1, 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Items.Count());
        Assert.Equal(15, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);

        var item = result.Items.First();
        Assert.Equal($"{users[0].FirstName} {users[0].LastName}", item.UserName);
        Assert.Equal(sentAt.ToShamsiDate(), item.CreatedAt);
        Assert.Equal(requests[1].Product.Name, item.ProductName);
        Assert.Equal(requests[1].ProductPrice.ToString("N0"), item.ProductPrice);
        Assert.Equal(requests[1].ProductTotalPrice.ToString("N0"), item.ProductTotalPrice);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
