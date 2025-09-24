using Goldix.Application.Queries.Notification;
using Goldix.Infrastructure.Handlers.QueryHandlers.Notification;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Notification;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Notification;

public class GetAllNotificationsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllNotificationsQueryHandler _handler;

    public GetAllNotificationsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new GetAllNotificationsQueryHandler(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoNotifications_ReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllNotificationsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenNotificationsExist_ReturnsPagedResult()
    {
        // Arrange
        await NotificationTestHelper.SeedNotificationsAsync(_db, 15);

        var query = new GetAllNotificationsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Items.Count());
        Assert.Equal(15, result.TotalCount);

        string senderName = result.Items.First().SenderName;
        Assert.NotNull(senderName);
        Assert.Equal("FirstName1 LastName1", senderName);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
