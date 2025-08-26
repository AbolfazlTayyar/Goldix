using Goldix.Application.Extensions;
using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Application.Wrappers;
using Goldix.Domain.Enums.Common;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Trade;

public class GetTradeRequestsBySearchQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetTradeRequestsBySearchQuery, PagedResult<TradeRequestDto>>
{
    public async Task<PagedResult<TradeRequestDto>> Handle(GetTradeRequestsBySearchQuery request, CancellationToken cancellationToken)
    {
        var status = RequestStatus.confirmed.ToDisplay();
        var startDate = request.dto.StartDate.ToMiladi();
        var endDate = request.dto.EndDate.ToMiladi();

        var baseQuery = db.TradeRequests
            .AsNoTracking()
            .Where(x =>
                    x.Status == status &&
                    (!startDate.HasValue || x.SentAt.Date >= startDate.Value.Date) &&
                    (!endDate.HasValue || x.SentAt.Date <= endDate.Value.Date));

        var count = await baseQuery.CountAsync(cancellationToken);

        var result = await baseQuery
            .Include(x => x.Product)
            .ProjectTo<TradeRequestDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TradeRequestDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
