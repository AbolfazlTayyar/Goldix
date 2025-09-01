using Goldix.Application.Queries.Group;
using Goldix.Infrastructure.Handlers.QueryHandlers.Group;
using Goldix.Infrastructure.Persistence;

namespace Goldix.UnitTests.Handlers.QueryHandlers.Group;

public class GetAllGroupsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly GetAllGroupsQueryHandler _handler;

    public GetAllGroupsQueryHandlerTests()
    {
        _context = TestHelpers.CreateInMemoryContext();
        _mapper = TestHelpers.CreateMapper();
        _handler = new GetAllGroupsQueryHandler(_context, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoGroups_ShouldReturnEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllGroupsQuery(page: 1, pageSize: 10);

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
    public async Task Handle_WhenGroupsExist_ShouldReturnCorrectPagedResult()
    {
        // Arrange
        await SeedGroupsAsync(5);
        var query = new GetAllGroupsQuery(page: 1, pageSize: 3);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Items.Count());
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(1, result.Page);
        Assert.Equal(3, result.PageSize);
    }

    [Fact]
    public async Task Handle_WhenRequestingSecondPage_ShouldReturnCorrectItems()
    {
        // Arrange
        await SeedGroupsAsync(10);
        var query = new GetAllGroupsQuery(page: 2, pageSize: 4);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Items.Count());
        Assert.Equal(10, result.TotalCount);
        Assert.Equal(2, result.Page);
        Assert.Equal(4, result.PageSize);

        // Verify we got different items than first page
        var firstPageQuery = new GetAllGroupsQuery(page: 1, pageSize: 4);
        var firstPageResult = await _handler.Handle(firstPageQuery, CancellationToken.None);
        var firstPageIds = firstPageResult.Items.Select(x => x.Id).ToList();
        var secondPageIds = result.Items.Select(x => x.Id).ToList();
        Assert.False(firstPageIds.Intersect(secondPageIds).Any(),
            "Second page should have different items than first page");
    }

    [Fact]
    public async Task Handle_WhenRequestingLastPage_ShouldReturnRemainingItems()
    {
        // Arrange
        await SeedGroupsAsync(7);
        var query = new GetAllGroupsQuery(page: 3, pageSize: 3);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Items.Count());
        Assert.Equal(7, result.TotalCount);
        Assert.Equal(3, result.Page);
        Assert.Equal(3, result.PageSize);
    }

    [Fact]
    public async Task Handle_WhenRequestingPageBeyondData_ShouldReturnEmptyItems()
    {
        // Arrange
        await SeedGroupsAsync(5);
        var query = new GetAllGroupsQuery(page: 10, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.Items.Count());
        Assert.Equal(5, result.TotalCount);
        Assert.Equal(10, result.Page);
        Assert.Equal(10, result.PageSize);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 5)]
    [InlineData(1, 20)]
    [InlineData(2, 10)]
    public async Task Handle_WithDifferentPageSizes_ShouldCalculatePaginationCorrectly(int page, int pageSize)
    {
        // Arrange
        var totalGroups = 15;
        await SeedGroupsAsync(totalGroups);
        var query = new GetAllGroupsQuery(page: page, pageSize: pageSize);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(totalGroups, result.TotalCount);
        Assert.Equal(page, result.Page);
        Assert.Equal(pageSize, result.PageSize);

        // Calculate expected items count
        var skip = (page - 1) * pageSize;
        var expectedCount = Math.Min(pageSize, Math.Max(0, totalGroups - skip));
        Assert.Equal(expectedCount, result.Items.Count());
    }

    [Fact]
    public async Task Handle_ShouldMapGroupsToGroupDto()
    {
        // Arrange
        var group = new Domain.Entities.User.Group
        {
            Id = 1,
            Name = "Test Group",
            BuyPriceDifferencePercent = 10,
            SellPriceDifferencePercent = 5,
            CreatedAt = DateTime.Now
        };
        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();

        var query = new GetAllGroupsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result.Items);

        var dto = result.Items.First();
        Assert.Equal(group.Id, dto.Id);
        Assert.Equal(group.Name, dto.Name);
        Assert.Equal(group.BuyPriceDifferencePercent.ToString(), dto.BuyPriceDifferencePercent);
        Assert.Equal(group.SellPriceDifferencePercent.ToString(), dto.SellPriceDifferencePercent);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    public async Task Handle_WithInvalidPaginationParameters_ShouldHandleGracefully(int page, int pageSize)
    {
        // Arrange
        await SeedGroupsAsync(5);
        var query = new GetAllGroupsQuery(page: page, pageSize: pageSize);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(page, result.Page);
        Assert.Equal(pageSize, result.PageSize);
    }

    [Fact]
    public async Task Handle_WithLargeDataset_ShouldPerformEfficiently()
    {
        // Arrange
        await SeedGroupsAsync(1000);
        var query = new GetAllGroupsQuery(page: 5, pageSize: 20);
        var stopwatch = Stopwatch.StartNew();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        stopwatch.Stop();
        Assert.NotNull(result);
        Assert.Equal(20, result.Items.Count());
        Assert.Equal(1000, result.TotalCount);

        // Performance assertion (adjust threshold as needed)
        Assert.True(stopwatch.ElapsedMilliseconds < 1000, "Query should complete within reasonable time");
    }

    private async Task SeedGroupsAsync(int count)
    {
        var groups = Enumerable.Range(1, count)
            .Select(i => new Domain.Entities.User.Group
            {
                Id = i,
                Name = $"Group {i}",
                BuyPriceDifferencePercent = i * 10,
                SellPriceDifferencePercent = i * 5,
                CreatedAt = DateTime.UtcNow.AddDays(-i)
            })
            .ToList();

        await _context.Groups.AddRangeAsync(groups);
        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
