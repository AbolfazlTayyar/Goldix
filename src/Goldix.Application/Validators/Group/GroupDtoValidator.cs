using Goldix.Domain.Constants;
using Goldix.Application.Extensions;
using Goldix.Application.Models.Group;

namespace Goldix.Application.Validators.Group;

public class GroupDtoValidator : AbstractValidator<CreateUpdateGroupDto>
{
    public GroupDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .WithName("نام گروه");

        RuleFor(x => x.BuyPriceDifferencePercent)
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت خرید صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت خرید باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("اختلاف با قیمت خرید (درصد)");

        RuleFor(x => x.SellPriceDifferencePercent)
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت فروش صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت فروش باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("اختلاف با قیمت فروش (درصد)");
    }
}
