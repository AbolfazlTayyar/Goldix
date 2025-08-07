using Goldix.Application.Models.Product;

namespace Goldix.Application.Commands.Product;

public record UpdateProductCommand(int id, ProductDto dto) : IRequest;
