using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.MeasurementUnit;

public static class MeasurementUnitTestHelper
{
    public static async Task SeedMeasurementUnitsAsync(ApplicationDbContext db, int count)
    {
        var measurementUnits = Enumerable.Range(1, count)
            .Select(i => new Domain.Entities.Product.MeasurementUnit
            {
                Name = $"Unit{i}",
                IsActive = true,
            })
            .ToList();

        await db.MeasurementUnits.AddRangeAsync(measurementUnits);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }
}