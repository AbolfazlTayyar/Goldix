using Goldix.Application.Models.Trade;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.Trade;

public record GetAllUserTradeRequestsQuery(string userId, int page, int pageSize) : IRequest<PagedResult<TradeRequestDto>>;
