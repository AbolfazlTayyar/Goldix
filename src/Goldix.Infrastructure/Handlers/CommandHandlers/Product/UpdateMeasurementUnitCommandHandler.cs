using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class UpdateMeasurementUnitCommandHandler(ApplicationDbContext db) : IRequestHandler<UpdateMeasurementUnitCommand>
{
    public async Task Handle(UpdateMeasurementUnitCommand request, CancellationToken cancellationToken)
    {
        var measurementUnit = await db.MeasurementUnits.FirstOrDefaultAsync(cancellationToken);
        if (measurementUnit is null)
            throw new NotFoundException();

        measurementUnit.Name = request.dto.Name;
        measurementUnit.IsActive = request.dto.IsActive;
        await db.SaveChangesAsync(cancellationToken);
    }
}
