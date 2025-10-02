using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Product;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Product;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product;

public class UpdateProductCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly UpdateProductCommandHandler _handler;

    public UpdateProductCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new UpdateProductCommandHandler(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new UpdateProductCommand(99, new ProductDto());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenProductExists_ShouldUpdateProduct()
    {
        // Arrange
        await ProductTestHelper.CreateProductsAsync(_db, 1);

        var updatedProductDto = new ProductDto
        {
            Name = "New Name",
            BuyPrice = "20.00",
            SellPrice = "30.00",
            TradingStartTime = new TimeSpan(9, 0, 0),
            TradingEndTime = new TimeSpan(18, 0, 0),
            IsActive = true,
            MeasurementUnitId = 2,
            Comment = "Updated Comment",
        };

        var command = new UpdateProductCommand(1, updatedProductDto);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedProduct = await _db.Products.FindAsync(1);
        Assert.NotNull(updatedProduct);
        Assert.Equal("New Name", updatedProduct.Name);
        Assert.Equal(20.00m, updatedProduct.BuyPrice);
        Assert.Equal(30.00m, updatedProduct.SellPrice);
        Assert.Equal(new TimeSpan(9, 0, 0), updatedProduct.TradingStartTime);
        Assert.Equal(new TimeSpan(18, 0, 0), updatedProduct.TradingEndTime);
        Assert.True(updatedProduct.IsActive);
        Assert.Equal(2, updatedProduct.MeasurementUnitId);
        Assert.Equal("Updated Comment", updatedProduct.Comment);
        Assert.NotNull(updatedProduct.LastModifiedAt);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
