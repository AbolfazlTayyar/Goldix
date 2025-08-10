using Goldix.Application.Models.Product;

namespace Goldix.Application.Commands.Product.MeasurementUnit;

public record UpdateMeasurementUnitCommand(MeasurementUnitDto dto) : IRequest;
