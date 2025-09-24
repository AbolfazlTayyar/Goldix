using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Product;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Product;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product;

public class ModifyProductPricesCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly ModifyProductPricesCommandHandler _handler;

    public ModifyProductPricesCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new ModifyProductPricesCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new ModifyProductPricesCommand
        (
            999,
            new PricingDto
            {
                BuyPrice = "100.00",
                SellPrice = "120.00",
            }
        );

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldModifyProductPrices()
    {
        // Arrange
        await ProductTestHelper.SeedProductsAsync(_db, 1);

        var command = new ModifyProductPricesCommand
        (
            1,
            new PricingDto
            {
                BuyPrice = "110.00",
                SellPrice = "130.00",
            }
        );

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedProduct = await _db.Products.FindAsync(1);
        Assert.NotNull(updatedProduct);
        Assert.Equal(110.00m, updatedProduct.BuyPrice);
        Assert.Equal(130.00m, updatedProduct.SellPrice);
        Assert.NotNull(updatedProduct.LastModifiedAt);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
