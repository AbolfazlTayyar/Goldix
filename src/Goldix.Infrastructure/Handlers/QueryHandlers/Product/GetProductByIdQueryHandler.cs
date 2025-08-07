using Goldix.Application.Exceptions;
using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Product;

public class GetProductByIdQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await db.Products
            .AsNoTracking()
            .Include(x => x.MeasurementUnit)
            .FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);

        if (product is null)
            throw new NotFoundException();

        var result = mapper.Map<ProductDto>(product);
        return result;
    }
}
