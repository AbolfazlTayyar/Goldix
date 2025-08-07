using Goldix.Application.Models.Product;

namespace Goldix.Application.Queries.Product;

public record GetProductByIdQuery(int id) : IRequest<ProductDto>;
