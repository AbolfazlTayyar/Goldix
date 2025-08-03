namespace Goldix.Domain.Enums.WalletManagement;

public enum WalletTransactionReason
{
    [Display(Name = "افزایش توسط ادمین")]
    increaseByAdmin = 0,

    [Display(Name = "کاهش توسط ادمین")]
    decreaseByAdmin = 1
}