using Goldix.Application.Queries.Product;
using Goldix.Infrastructure.Handlers.QueryHandlers.Product;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Product;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Product;

public class GetProductByIdQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetProductByIdQueryHandler _handler;

    public GetProductByIdQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoProduct_ShouldThrowNotFoundException()
    {
        // Arrange
        var query = new GetProductByIdQuery(1);

        // Act & Assert
        await Assert.ThrowsAsync<Goldix.Application.Exceptions.NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenProductExists_ShouldReturnProduct()
    {
        // Arrange
        await ProductTestHelper.CreateProductsAsync(_db, 3);

        var query = new GetProductByIdQuery(1);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Product1", result.Name);
        Assert.Equal(11.ToString(), result.BuyPrice);
        Assert.Equal(16.ToString(), result.SellPrice);
        Assert.Equal(1, result.MeasurementUnitId);
        Assert.Equal("Unit1", result.MeasurementUnitName);
        Assert.True(result.IsActive);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
