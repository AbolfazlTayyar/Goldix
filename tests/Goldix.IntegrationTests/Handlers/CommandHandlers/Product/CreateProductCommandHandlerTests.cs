using Goldix.Application.Commands.Product;
using Goldix.Application.Models.Product;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product;

public class CreateProductCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldCreateProduct()
    {
        // Arrange
        var command = new CreateProductCommand
        (
            new ProductDto
            {
                Name = "Test Product",
                BuyPrice = "90.0",
                SellPrice = "100.0",
                TradingStartTime = TimeSpan.Parse("09:00"),
                TradingEndTime = TimeSpan.Parse("17:00"),
                IsActive = true,
                MeasurementUnitId = 1,
                Comment = "Test Description"
            }
        );

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var product = await _db.Products.FindAsync(1);
        Assert.NotNull(product);
        Assert.Equal("Test Product", product.Name);
        Assert.Equal(90.0m, product.BuyPrice);
        Assert.Equal(100.0m, product.SellPrice);
        Assert.Equal(TimeSpan.Parse("09:00"), product.TradingStartTime);
        Assert.Equal(TimeSpan.Parse("17:00"), product.TradingEndTime);
        Assert.True(product.IsActive);
        Assert.Equal(1, product.MeasurementUnitId);
        Assert.Equal("Test Description", product.Comment);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
