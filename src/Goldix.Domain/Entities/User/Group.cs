using Goldix.Domain.Common;

namespace Goldix.Domain.Entities.User;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal BuyPriceDifferencePercent { get; set; }
    public decimal SellPriceDifferencePercent { get; set; }

    #region Navigation Properties
    public List<ApplicationUser> Users { get; set; }
    #endregion
}
