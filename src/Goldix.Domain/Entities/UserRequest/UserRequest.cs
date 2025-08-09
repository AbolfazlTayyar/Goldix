using Goldix.Domain.Common;
using Goldix.Domain.Entities.User;
using Goldix.Domain.Entities.WalletManagement;

namespace Goldix.Domain.Entities.UserRequest;

public class UserRequest : BaseEntity
{
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ProductId { get; set; }
    public byte ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
    public decimal ProductTotalPrice { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }

    #region Navigation Properties
    public ApplicationUser User { get; set; }
    public WalletTransaction WalletTransaction { get; set; }
    public Product.Product Product { get; set; }
    #endregion
}