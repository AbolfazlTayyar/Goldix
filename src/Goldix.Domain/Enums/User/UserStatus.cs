namespace Goldix.Domain.Enums.User;

public enum UserStatus
{
    [Display(Name = "در انتظار تایید")]
    waiting = 0,

    [Display(Name = "تایید شده")]
    confirmed = 1,

    [Display(Name = "رد شده")]
    rejected = 2,
}
