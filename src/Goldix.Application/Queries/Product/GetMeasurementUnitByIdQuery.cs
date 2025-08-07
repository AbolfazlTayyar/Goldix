using Goldix.Application.Models.Product;

namespace Goldix.Application.Queries.Product;

public record GetMeasurementUnitByIdQuery(int id) : IRequest<MeasurementUnitDto>;
