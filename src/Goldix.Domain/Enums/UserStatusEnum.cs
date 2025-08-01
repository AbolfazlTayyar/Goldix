using System.ComponentModel.DataAnnotations;

namespace Goldix.Domain.Enums;

public enum UserStatusEnum
{
    [Display(Name = "در انتظار تایید")]
    waiting = 0,

    [Display(Name = "تایید شده")]
    confirmed = 1,

    [Display(Name = "رد شده")]
    rejected = 2,
}
