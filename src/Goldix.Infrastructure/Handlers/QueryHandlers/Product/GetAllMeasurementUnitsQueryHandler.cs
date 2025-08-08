using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Product;

public class GetAllMeasurementUnitsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllMeasurementUnitsQuery, PagedResult<MeasurementUnitDto>>
{
    public async Task<PagedResult<MeasurementUnitDto>> Handle(GetAllMeasurementUnitsQuery request, CancellationToken cancellationToken)
    {
        var count = await db.MeasurementUnits
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var result = await db.MeasurementUnits
            .AsNoTracking()
            .ProjectTo<MeasurementUnitDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<MeasurementUnitDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
