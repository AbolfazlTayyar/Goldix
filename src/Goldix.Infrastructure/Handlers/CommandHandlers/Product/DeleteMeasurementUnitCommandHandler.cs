using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class DeleteMeasurementUnitCommandHandler(ApplicationDbContext db) : IRequestHandler<DeleteMeasurementUnitCommand>
{
    public async Task Handle(DeleteMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        var measurementUnit = await db.MeasurementUnits.FirstOrDefaultAsync(x=>x.Id == request.id, cancellationToken);
        if (measurementUnit is null)
            throw new NotFoundException();

        db.MeasurementUnits.Remove(measurementUnit);
        await db.SaveChangesAsync(cancellationToken);
    }
}