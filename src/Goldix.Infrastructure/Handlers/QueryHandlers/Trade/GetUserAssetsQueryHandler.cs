using Goldix.Application.Models.Trade;
using Goldix.Application.Queries.Trade;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Trade;

public class GetUserAssetsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetUserAssetsQuery, List<AssetDto>>
{
    public async Task<List<AssetDto>> Handle(GetUserAssetsQuery request, CancellationToken cancellationToken)
    {
        var result = await db.Assets
            .Where(x => x.UserId == request.UserId)
            .Include(x => x.Product)
            .ProjectTo<AssetDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
