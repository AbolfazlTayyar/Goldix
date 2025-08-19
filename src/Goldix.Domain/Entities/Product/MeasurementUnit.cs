using Goldix.Domain.Entities.Common;

namespace Goldix.Domain.Entities.Product;

public class MeasurementUnit : BaseEntity
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;

    #region Navigation Properties
    public List<Product> Products { get; set; }
    #endregion
}
