using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Helpers.Extensions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.UserRequest;

public class GetAllRequestsByStatusQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllTradeRequestsByStatusQuery, PagedResult<TradeRequestDto>>
{
    public async Task<PagedResult<TradeRequestDto>> Handle(GetAllTradeRequestsByStatusQuery request, CancellationToken cancellationToken)
    {
        var status = (request.dto.Status).ToDisplay();

        var baseQuery = db.TradeRequests
            .AsNoTracking()
            .Where(x => x.Status == status && x.Sender.IsActive);

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
