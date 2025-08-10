using Goldix.Application.Commands.Product.MeasurementUnit;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product.MeasurementUnit;

public class CreateMeasurementUnitCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<CreateMeasurementUnitCommand>
{
    public async Task Handle(CreateMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        var measurementUnit = mapper.Map<Domain.Entities.Product.MeasurementUnit>(request.dto);
        await db.AddAsync(measurementUnit, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
