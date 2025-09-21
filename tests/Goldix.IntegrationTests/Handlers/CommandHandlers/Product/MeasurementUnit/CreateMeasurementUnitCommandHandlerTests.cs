using Goldix.Application.Commands.Product.MeasurementUnit;
using Goldix.Application.Models.Product;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product.MeasurementUnit;
using Goldix.Infrastructure.Persistence;
using Goldix.UnitTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product.MeasurementUnit;

public class CreateMeasurementUnitCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly CreateMeasurementUnitCommandHandler _handler;

    public CreateMeasurementUnitCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldCreateMeasurementUnit()
    {
        // Arrange
        var command = new CreateMeasurementUnitCommand
        (
            new MeasurementUnitDto
            {
                Name = "Kilogram",
                IsActive = true,
            }
        );

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var measurementUnit = await _db.MeasurementUnits.FindAsync(1);
        Assert.NotNull(measurementUnit);
        Assert.Equal("Kilogram", measurementUnit.Name);
        Assert.True(measurementUnit.IsActive);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
