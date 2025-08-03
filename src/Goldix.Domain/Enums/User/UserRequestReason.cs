namespace Goldix.Domain.Enums.User;

public enum UserRequestReason
{
    [Display(Name = "خرید")]
    buy = 1,

    [Display(Name = "فروش")]
    sell = 2,

    [Display(Name = "افزایش موجودی کیف پول")]
    topUp = 4
}
