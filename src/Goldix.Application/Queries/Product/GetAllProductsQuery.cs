using Goldix.Application.Models.Product;

namespace Goldix.Application.Queries.Product;

public record GetAllProductsQuery : IRequest<List<ProductDto>>;
