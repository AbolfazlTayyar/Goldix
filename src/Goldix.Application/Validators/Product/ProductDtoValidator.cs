using Goldix.Application.Models.Product;
using Goldix.Domain.Constants;
using Goldix.Application.Extensions;

namespace Goldix.Application.Validators.Product;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .WithName("نام");

        RuleFor(x => x.BuyPrice)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت خرید صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت خرید باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار در قیمت خرید مجاز است")
            .WithName("قیمت خرید");

        RuleFor(x => x.SellPrice)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت قیمت فروش صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("قیمت فروش باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار در قیمت فروش مجاز است")
            .WithName("قیمت فروش");

        RuleFor(x => x.TradingStartTime)
            .NotEmpty()
            .WithName("ساعت شروع معاملات");

        RuleFor(x => x.TradingEndTime)
            .NotEmpty()
            .WithName("ساعت پایان معاملات");

        RuleFor(x => x.MeasurementUnitId)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithName("واحد");

        RuleFor(x => x.IsActive)
            .NotEmpty()
            .WithName("وضعیت(فعال/غیرفغال)");
    }
}
