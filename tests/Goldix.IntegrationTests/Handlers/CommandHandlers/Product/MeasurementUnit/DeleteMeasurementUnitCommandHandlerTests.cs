using Goldix.Application.Commands.Product.MeasurementUnit;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product.MeasurementUnit;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.MeasurementUnit;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product.MeasurementUnit;

public class DeleteMeasurementUnitCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly DeleteMeasurementUnitCommandHandler _handler;

    public DeleteMeasurementUnitCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new(_db);
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var command = new DeleteMeasurementUnitCommand(99);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitExists_ShouldDeleteMeasurementUnit()
    {
        // Arrange
        await MeasurementUnitTestHelper.SeedMeasurementUnitsAsync(_db, 1);

        var command = new DeleteMeasurementUnitCommand(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedMeasurementUnit = await _db.MeasurementUnits.FindAsync(1);
        Assert.Null(deletedMeasurementUnit);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
