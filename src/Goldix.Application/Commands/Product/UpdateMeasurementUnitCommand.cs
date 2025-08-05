using Goldix.Application.Models.Product;

namespace Goldix.Application.Commands.Product;

public record UpdateMeasurementUnitCommand(MeasurementUnitDto dto) : IRequest;
