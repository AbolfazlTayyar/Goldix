using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class ModifyProductPricesCommandHandler(ApplicationDbContext db) : IRequestHandler<ModifyProductPricesCommand>
{
    public async Task Handle(ModifyProductPricesCommand request, CancellationToken cancellationToken)
    {
        var record = await db.Products.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (record is null)
            throw new NotFoundException();

        record.BuyPrice = decimal.Parse(request.dto.BuyPrice);
        record.SellPrice = decimal.Parse(request.dto.SellPrice);

        record.LastModifiedDate = DateTime.Now;

        await db.SaveChangesAsync(cancellationToken);
    }
}
