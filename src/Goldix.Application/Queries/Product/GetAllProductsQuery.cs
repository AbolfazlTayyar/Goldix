using Goldix.Application.Models.Product;
using Goldix.Application.Wrappers;

namespace Goldix.Application.Queries.Product;

public record GetAllProductsQuery(int page, int pageSize) : IRequest<PagedResult<ProductDto>>;
