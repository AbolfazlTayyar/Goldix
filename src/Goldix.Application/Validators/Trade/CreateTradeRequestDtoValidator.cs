using Goldix.Application.Extensions;
using Goldix.Application.Models.Trade;
using Goldix.Domain.Constants;

namespace Goldix.Application.Validators.Trade;

public class CreateTradeRequestDtoValidator : AbstractValidator<CreateTradeRequestDto>
{
    public CreateTradeRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithName("شناسه کالا");

        RuleFor(x => x.ProductCount)
            .NotEmpty()
            .GreaterThanOrEqualTo((byte)1)
            .WithName("تعداد");

        RuleFor(x => x.ProductPrice)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت فروش صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت فروش باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("مبلغ");

        RuleFor(x => x.Type)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_ENUM_LENGTH)
            .WithName("نوع معامله");
    }
}
