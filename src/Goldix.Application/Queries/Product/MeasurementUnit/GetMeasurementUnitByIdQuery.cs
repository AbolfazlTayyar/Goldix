using Goldix.Application.Models.Product;

namespace Goldix.Application.Queries.Product.MeasurementUnit;

public record GetMeasurementUnitByIdQuery(int id) : IRequest<MeasurementUnitDto>;
