using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.Product;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product;

public class DeleteProductCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new DeleteProductCommandHandler(_db);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeleteProductCommand(99);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenProductExists_ShouldDeleteProduct()
    {
        // Arrange
        await ProductTestHelper.CreateProductsAsync(_db, 1);

        var command = new DeleteProductCommand(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedProduct = await _db.Products.FindAsync(1);
        Assert.Null(deletedProduct);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
