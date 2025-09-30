using Goldix.Application.Extensions;
using Goldix.Application.Queries.User;
using Goldix.Infrastructure.Handlers.QueryHandlers.User;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;
using Goldix.IntegrationTests.Helpers.Notification;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.User;

public class GetUserNotificationsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetUserNotificationsQueryHandler _handler;

    public GetUserNotificationsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoNotificationsExist_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var query = new GetUserNotificationsQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_WhenNotificationsExist_ShouldReturnPagedResult()
    {
        // Arrange
        var notificationContent = await NotificationTestHelper.SeedNotificationsAsync(_db, 1);
        var userNotification = await NotificationTestHelper.SeedUserNotificationsAsync(_db, 1, false, "test-user-id", notificationContent.First());

        var query = new GetUserNotificationsQuery("test-user-id");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);

        var item = result.First();
        Assert.Equal("FirstName1 LastName1", item.SenderName);
        Assert.Equal("Notification 1", item.Title);
        Assert.Equal("This is the description for notification 1.", item.Description);
        Assert.Equal(notificationContent.First().CreatedAt.ToShamsiDate(), item.CreatedAt);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
