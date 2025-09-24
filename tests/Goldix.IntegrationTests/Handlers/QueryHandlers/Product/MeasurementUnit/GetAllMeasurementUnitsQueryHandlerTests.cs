using Goldix.Application.Queries.Product.MeasurementUnit;
using Goldix.Infrastructure.Handlers.QueryHandlers.Product.MeasurementUnit;
using Goldix.Infrastructure.Persistence;
using Goldix.IntegrationTests.Helpers.MeasurementUnit;
using Goldix.IntegrationTests.Helpers;

namespace Goldix.IntegrationTests.Handlers.QueryHandlers.Product.MeasurementUnit;

public class GetAllMeasurementUnitsQueryHandlerTests : IDisposable
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly GetAllMeasurementUnitsQueryHandler _handler;

    public GetAllMeasurementUnitsQueryHandlerTests()
    {
        _db = BaseHelper.CreateInMemoryContext();
        _mapper = BaseHelper.CreateMapper();
        _handler = new(_db, _mapper);
    }

    [Fact]
    public async Task Handle_WhenNoMeasurementUnits_ReturnsEmptyPagedResult()
    {
        // Arrange
        var query = new GetAllMeasurementUnitsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
    }

    [Fact]
    public async Task Handle_WhenMeasurementUnitsExist_ReturnsPagedResult()
    {
        // Arrange
        await MeasurementUnitTestHelper.SeedMeasurementUnitsAsync(_db, 15);

        var query = new GetAllMeasurementUnitsQuery(page: 1, pageSize: 10);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Items.Count());
        Assert.Equal(15, result.TotalCount);

        var measurementUnit = result.Items.First();
        Assert.NotNull(measurementUnit.Name);
        Assert.Equal("Unit1", measurementUnit.Name);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
