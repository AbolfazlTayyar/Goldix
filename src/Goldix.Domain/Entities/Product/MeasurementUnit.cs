using Goldix.Domain.Common;

namespace Goldix.Domain.Entities.Product;

public class MeasurementUnit : BaseEntity
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
