using Goldix.Application.Models.Product;
using Goldix.Application.Extensions;

namespace Goldix.Application.Validators.Product;

public class PricingDtoValidator : AbstractValidator<PricingDto>
{
    public PricingDtoValidator()
    {
        RuleFor(x => x.BuyPrice)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت خرید صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت خرید باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("قیمت خرید");

        RuleFor(x => x.SellPrice)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت فروش صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت فروش باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("قیمت فروش");
    }
}
