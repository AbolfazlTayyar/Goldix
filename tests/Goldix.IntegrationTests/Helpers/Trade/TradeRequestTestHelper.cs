using Goldix.Domain.Entities.Trade;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.Common;
using Goldix.Domain.Enums.User;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.Trade;

public static class TradeRequestTestHelper
{
    public static async Task SeedTradeRequestsAsync(ApplicationDbContext db, int count, RequestStatus status, DateTime createdAt, string userId = null, bool isSenderActive = true)
    {
        ApplicationUser sender = null;

        if (userId != null)
        {
            sender = new ApplicationUser
            {
                Id = userId,
                FirstName = "FirstName",
                LastName = "LastName",
                CreatedAt = createdAt,
                IsActive = isSenderActive,
                Status = UserStatus.confirmed.ToDisplay(),
            };
        }

        var tradeRequests = Enumerable.Range(1, count)
            .Select(i => new TradeRequest
            {
                SenderId = userId ?? Guid.NewGuid().ToString(),
                Sender = sender ?? new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    CreatedAt = createdAt,
                    IsActive = isSenderActive,
                    Status = UserStatus.confirmed.ToDisplay(),
                },
                ReceiverId = Guid.NewGuid().ToString(),
                SentAt = createdAt,
                Status = status.ToDisplay(),
                ProductId = i,
                ProductCount = (byte)i,
                ProductPrice = 10000.0m + i,
                ProductTotalPrice = (10000.0m + i) * ((byte)i),
                Type = "sell",
                Product = new Domain.Entities.Product.Product
                {
                    Name = $"Product {i}",
                    BuyPrice = 10.0m + i,
                    SellPrice = 15.0m + i,
                    CreatedAt = createdAt,
                    IsActive = true
                },
            })
            .ToList();

        await db.TradeRequests.AddRangeAsync(tradeRequests);
        await db.SaveChangesAsync();

        db.ChangeTracker.Clear();
    }

}