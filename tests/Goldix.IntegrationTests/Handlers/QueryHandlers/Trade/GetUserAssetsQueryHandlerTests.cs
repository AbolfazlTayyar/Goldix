using Goldix.Application.Queries.Trade;
using Goldix.Infrastructure.Handlers.QueryHandlers.Trade;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Trade;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Trade;

public class GetUserAssetsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetUserAssetsQueryHandler _handler;

    public GetUserAssetsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoAssetsExist_ReturnsEmptyList()
    {
        // Arrange
        var query = new GetUserAssetsQuery(Guid.NewGuid().ToString());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_WhenNoAssetsForGivenUserId_ReturnsEmptyPagedResult()
    {
        // Arrange
        await AssetTestHelper.SeedAssetsAsync(_db, 5);

        var query = new GetUserAssetsQuery(Guid.NewGuid().ToString());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_WhenAssetsExistForGivenUserId_ReturnsAssets()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        await AssetTestHelper.SeedAssetsAsync(_db, 5, userId);

        var query = new GetUserAssetsQuery(userId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
