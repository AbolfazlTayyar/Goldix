using Goldix.Domain.Entities.Trade;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Enums.Common;
using Goldix.Domain.Enums.Trade;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.IntegrationTests.Helpers.UserRequest;

public static class UserRequestTestHelper
{
    public static async Task<List<TradeRequest>> CreateUserRequestsAsync(ApplicationDbContext db, int count, ApplicationUser sender = null, string requestStatus = null, Domain.Entities.Product.Product product = null, DateTime? sentAt = null)
    {
        var requests = Enumerable.Range(1, count)
            .Select(i => new TradeRequest
            {
                Status = requestStatus ?? (i % 2 == 0 ? RequestStatus.pending.ToDisplay() : RequestStatus.rejected.ToDisplay()),
                SenderId = sender != null ? sender.Id : "",
                Product = product ?? null,
                SentAt = sentAt ?? DateTime.Now,
                Type = TradeRequestType.buy.ToDisplay(),
                ProductCount = (byte)i,
                ProductPrice = i * 1000,
                ProductTotalPrice = i * 1000 * i
            })
            .OrderBy(r => r.Id)
            .ToList();

        await db.TradeRequests.AddRangeAsync(requests);
        await db.SaveChangesAsync();

        return requests;
    }
}
