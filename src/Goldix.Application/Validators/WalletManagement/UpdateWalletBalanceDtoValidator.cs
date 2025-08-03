using Goldix.Application.Models.WalletManagement;

namespace Goldix.Application.Validators.WalletManagement;

public class UpdateWalletBalanceDtoValidator : AbstractValidator<UpdateWalletBalanceDto>
{
    public UpdateWalletBalanceDtoValidator()
    {
        RuleFor(x => x.NewBalance)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(BeValidDecimal).WithMessage("فرمت مبلغ صحیح نیست")
            .Must(BePositive).WithMessage("مبلغ باید مثبت باشد")
            .Must(HaveCorrectPrecision).WithMessage("حداکثر ۲ رقم اعشار مجاز است")
            .WithName("موجودی کیف پول");
    }

    private bool BeValidDecimal(string value)
    => decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out _);

    private bool BePositive(string value)
        => decimal.TryParse(value, out var result) && result > 0;

    private bool HaveCorrectPrecision(string value)
        => decimal.TryParse(value, out var result) &&
           decimal.GetBits(result)[3] >> 16 <= 2; // Max 2 decimal places
}
