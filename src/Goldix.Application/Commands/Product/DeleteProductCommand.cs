namespace Goldix.Application.Commands.Product;

public record DeleteProductCommand(int id) : IRequest;
