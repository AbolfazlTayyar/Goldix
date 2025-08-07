using Goldix.Application.Models.WalletManagement;
using Goldix.Application.Extensions;

namespace Goldix.Application.Validators.WalletManagement;

public class UpdateWalletBalanceDtoValidator : AbstractValidator<UpdateWalletBalanceDto>
{
    public UpdateWalletBalanceDtoValidator()
    {
        RuleFor(x => x.NewBalance)
            .NotNull()
            .Must(x => x.IsValidDecimal()).WithMessage("فرمت مبلغ صحیح نیست")
            .Must(x => x.IsPositiveDecimal()).WithMessage("مبلغ باید مثبت باشد")
            .Must(x=>x.HaveCorrectPrecision()).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("موجودی کیف پول");
    }
}
