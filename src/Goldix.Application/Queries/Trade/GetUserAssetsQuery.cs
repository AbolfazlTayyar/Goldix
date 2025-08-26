using Goldix.Application.Models.Trade;

namespace Goldix.Application.Queries.Trade;

public record GetUserAssetsQuery(string UserId) : IRequest<List<AssetDto>>;
