using Goldix.Application.Commands.Product;
using Goldix.Domain.Entities.Product;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class CreateMeasurementUnitCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<CreateMeasurementUnitCommand>
{
    public async Task Handle(CreateMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        var measurementUnit = mapper.Map<MeasurementUnit>(request.dto);
        await db.AddAsync(measurementUnit, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
