using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class UpdateProductCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (product is null)
            throw new NotFoundException();

        product.LastModifiedAt = DateTime.Now;

        mapper.Map(request.dto, product);
        await db.SaveChangesAsync(cancellationToken);
    }
}
