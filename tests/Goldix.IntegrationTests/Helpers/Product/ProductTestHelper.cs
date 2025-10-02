using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.Product;

public static class ProductTestHelper
{
    public static async Task<List<Domain.Entities.Product.Product>> CreateProductsAsync(ApplicationDbContext db, int count)
    {
        var products = Enumerable.Range(1, count)
            .Select(i => new Domain.Entities.Product.Product
            {
                Name = $"Product{i}",
                BuyPrice = 10 + i,
                SellPrice = 15 + i,
                CreatedAt = DateTime.Now,
                IsActive = true,
                MeasurementUnit = new Domain.Entities.Product.MeasurementUnit
                {
                    Name = $"Unit{i}"
                },
            })
            .ToList();

        await db.Products.AddRangeAsync(products);
        await db.SaveChangesAsync();

        return products;
    }
}
