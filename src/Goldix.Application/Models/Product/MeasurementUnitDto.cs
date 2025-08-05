using Goldix.Application.Mappings.Common;
using Goldix.Domain.Entities.Product;

namespace Goldix.Application.Models.Product;

public class MeasurementUnitDto : IMapFrom<MeasurementUnit>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
