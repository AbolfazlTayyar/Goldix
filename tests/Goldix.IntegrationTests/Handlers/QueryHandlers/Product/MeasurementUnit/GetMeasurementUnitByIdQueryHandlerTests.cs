using Goldix.Application.Exceptions;
using Goldix.Application.Queries.Product.MeasurementUnit;
using Goldix.Infrastructure.Handlers.QueryHandlers.Product.MeasurementUnit;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.MeasurementUnit;
using Goldix.UnitTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Product.MeasurementUnit;

public class GetMeasurementUnitByIdQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetMeasurementUnitByIdQueryHandler _handler;

    public GetMeasurementUnitByIdQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var query = new GetMeasurementUnitByIdQuery(1);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitExists_ShouldReturnMeasurementUnit()
    {
        // Arrange
        await MeasurementUnitTestHelper.SeedMeasurementUnitsAsync(_db, 1);

        var query = new GetMeasurementUnitByIdQuery(1);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Unit1", result.Name);
        Assert.True(result.IsActive);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
