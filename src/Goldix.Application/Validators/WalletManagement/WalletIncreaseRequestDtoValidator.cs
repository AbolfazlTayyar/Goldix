using Goldix.Application.Extensions;
using Goldix.Application.Models.WalletManagement;

namespace Goldix.Application.Validators.WalletManagement;

public class WalletIncreaseRequestDtoValidator : AbstractValidator<WalletIncreaseRequestDto>
{
    public WalletIncreaseRequestDtoValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت مبلغ صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("مبلغ باید مثبت باشد")
            .Must(x => x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("موجودی کیف پول");
    }
}
