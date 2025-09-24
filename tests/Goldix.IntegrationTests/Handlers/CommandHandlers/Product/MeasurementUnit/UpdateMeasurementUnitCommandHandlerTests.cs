using Goldix.Application.Commands.Product.MeasurementUnit;
using Goldix.Application.Exceptions;
using Goldix.Application.Models.Product;
using Goldix.Infrastructure.Handlers.CommandHandlers.Product.MeasurementUnit;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.MeasurementUnit;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.CommandHandlers.Product.MeasurementUnit;

public class UpdateMeasurementUnitCommandHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly UpdateMeasurementUnitCommandHandler _handler;

    public UpdateMeasurementUnitCommandHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _handler = new(_db);
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitDoesNotExist_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var command = new UpdateMeasurementUnitCommand(new MeasurementUnitDto());

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitExists_ShouldUpdateMeasurementUnit()
    {
        // Arrange
        await MeasurementUnitTestHelper.SeedMeasurementUnitsAsync(_db, 1);

        var command = new UpdateMeasurementUnitCommand(new MeasurementUnitDto
        {
            Id = 1,
            Name = "New Name",
            IsActive = true
        });

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedMeasurementUnit = await _db.MeasurementUnits.FindAsync(1);
        Assert.NotNull(updatedMeasurementUnit);
        Assert.Equal("New Name", updatedMeasurementUnit.Name);
        Assert.True(updatedMeasurementUnit.IsActive);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
