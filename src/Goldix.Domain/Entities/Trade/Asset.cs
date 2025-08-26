using Goldix.Domain.Entities.Common;
using Goldix.Domain.Entities.User;

namespace Goldix.Domain.Entities.Trade;

public class Asset : BaseEntity
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }

    #region Navigation Properties
    public ApplicationUser User { get; set; }
    public Product.Product Product { get; set; }
    #endregion
}
