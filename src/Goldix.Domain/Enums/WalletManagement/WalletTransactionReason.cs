namespace Goldix.Domain.Enums.WalletManagement;

public enum WalletTransactionReason
{
    [Display(Name = "افزایش توسط ادمین")]
    increaseByAdmin = 0,

    [Display(Name = "کاهش توسط ادمین")]
    decreaseByAdmin = 1,

    [Display(Name = "خرید محصول")]
    buyProduct = 2,

    [Display(Name = "فروش محصول")]
    sellProduct = 3,

    [Display(Name = "افزایش بدلیل درخواست")]
    increaseByRequest = 4,
}