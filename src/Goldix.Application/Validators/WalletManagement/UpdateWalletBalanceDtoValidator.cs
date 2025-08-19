using Goldix.Application.Models.WalletManagement;
using Goldix.Application.Extensions;
using Goldix.Domain.Constants;

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

        RuleFor(x => x.AdminId)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .WithName("شناسه ادمین");
    }
}
