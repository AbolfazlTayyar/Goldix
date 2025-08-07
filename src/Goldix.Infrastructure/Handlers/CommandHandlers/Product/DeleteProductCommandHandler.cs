using Goldix.Application.Commands.Product;
using Goldix.Application.Exceptions;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class DeleteProductCommandHandler(ApplicationDbContext db) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
        if (product is null)
            throw new NotFoundException();

        db.Products.Remove(product);
        await db.SaveChangesAsync(cancellationToken);   
    }
}
