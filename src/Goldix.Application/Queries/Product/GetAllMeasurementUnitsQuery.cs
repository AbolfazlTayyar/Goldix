using Goldix.Application.Models.Product;

namespace Goldix.Application.Queries.Product;

public record GetAllMeasurementUnitsQuery : IRequest<List<MeasurementUnitDto>>;
