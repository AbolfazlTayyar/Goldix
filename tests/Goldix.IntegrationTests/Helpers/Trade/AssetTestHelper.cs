using Goldix.Domain.Entities.Trade;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.Trade;

public static class AssetTestHelper
{
    public static async Task SeedAssetsAsync(ApplicationDbContext db, int count, string userId = null)
    {
        var assets = Enumerable.Range(1, count)
            .Select(i => new Asset
            {
                UserId = userId ?? Guid.NewGuid().ToString(),
                ProductId = i,
                Count = i * 10,
                Product = new Domain.Entities.Product.Product
                {
                    Name = $"Product {i}",
                    BuyPrice = i * 100,
                    SellPrice = i * 90,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    MeasurementUnitId = 1
                }
            })
            .ToList();

        await db.Assets.AddRangeAsync(assets);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }
}
