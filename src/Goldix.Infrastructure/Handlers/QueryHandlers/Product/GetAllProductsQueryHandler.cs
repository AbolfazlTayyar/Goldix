using Goldix.Application.Models.Product;
using Goldix.Application.Queries.Product;
using Goldix.Application.Wrappers;
using Goldix.Infrastructure.Persistence;

namespace Goldix.Infrastructure.Handlers.QueryHandlers.Product;

public class GetAllProductsQueryHandler(ApplicationDbContext db, IMapper mapper) : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
{
    public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var count = await db.Products
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var result = await db.Products
            .AsNoTracking()
            .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
            .Skip((request.page - 1) * request.pageSize)
            .Take(request.pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductDto>
        {
            Items = result,
            TotalCount = count,
            Page = request.page,
            PageSize = request.pageSize,
        };
    }
}
