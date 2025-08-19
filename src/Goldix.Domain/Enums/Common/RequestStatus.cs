namespace Goldix.Domain.Enums.Common;

public enum RequestStatus
{
    [Display(Name = "در صف انتظار")]
    pending = 0,

    [Display(Name = "تایید شده")]
    confirmed = 1,

    [Display(Name = "رد شده")]
    rejected = 2,
}