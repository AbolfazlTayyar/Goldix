using Goldix.Application.Commands.Product;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.CommandHandlers.Product;

public class CreateProductCommandHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Domain.Entities.Product.Product>(request.dto);

        product.CreateDate = DateTime.Now;

        await db.AddAsync(product, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
