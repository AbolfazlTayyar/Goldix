using Goldix.Application.Exceptions;
using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Product;

public class GetMeasurementUnitByIdQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetMeasurementUnitByIdQuery, MeasurementUnitDto>
{
    public async Task<MeasurementUnitDto> Handle(GetMeasurementUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var measurementUnit = await db.MeasurementUnits
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (measurementUnit is null)
            throw new NotFoundException();

        var record = mapper.Map<MeasurementUnitDto>(measurementUnit);
        return record;
    }
}
