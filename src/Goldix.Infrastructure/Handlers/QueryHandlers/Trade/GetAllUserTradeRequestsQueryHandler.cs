using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Trade;

public class GetAllUserTradeRequestsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllUserTradeRequestsQuery, PagedResult<TradeRequestDto>>
{
    public async Task<PagedResult<TradeRequestDto>> Handle(GetAllUserTradeRequestsQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = db.TradeRequests
            .AsNoTracking()
            .Where(x => x.SenderId == request.userId);

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
