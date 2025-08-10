using Goldix.Application.Models.Product;

namespace Goldix.Application.Commands.Product.MeasurementUnit;

public record CreateMeasurementUnitCommand(MeasurementUnitDto dto) : IRequest;
