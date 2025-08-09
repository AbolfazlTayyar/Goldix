using Goldix.Application.Models.Product;

namespace Goldix.Application.Commands.Product;

public record ModifyProductPricesCommand(int id, PricingDto dto) : IRequest;
