using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Product;

public class GetAllMeasurementUnitsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllMeasurementUnitsQuery, List<MeasurementUnitDto>>
{
    public async Task<List<MeasurementUnitDto>> Handle(GetAllMeasurementUnitsQuery request, CancellationToken cancellationToken)
    {
        var result = await db.MeasurementUnits
            .AsNoTracking()
            .ProjectTo<MeasurementUnitDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return result;
    }
}
