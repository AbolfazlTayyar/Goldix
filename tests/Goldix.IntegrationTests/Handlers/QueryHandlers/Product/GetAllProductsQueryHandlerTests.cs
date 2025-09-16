using Goldix.Application.Queries.Product;
using Goldix.Infrastructure.Handlers.QueryHandlers.Product;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Product;
using Goldix.UnitTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Product;

public class GetAllProductsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllProductsQueryHandler _handler;

    public GetAllProductsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoProducts_ReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllProductsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenProductsExist_ReturnsPagedResult()
    {
        // Arrange
        await ProductTestHelper.SeedProductsAsync(_db, 15);

        var query = new GetAllProductsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Items.Count());
        Assert.Equal(15, result.TotalCount);

        var product = result.Items.First();
        Assert.NotNull(product.Name);
        Assert.Equal("Product1", product.Name);
        Assert.Equal("Unit1", product.MeasurementUnitName);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
