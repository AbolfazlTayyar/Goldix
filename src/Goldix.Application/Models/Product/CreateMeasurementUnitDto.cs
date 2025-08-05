using Goldix.Application.Mappings.Common;
using Goldix.Domain.Entities.Product;

namespace Goldix.Application.Models.Product;

public class CreateMeasurementUnitDto : IMapFrom<MeasurementUnit>
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
