using Goldix.Application.Models.WalletManagement;
using Goldix.Domain.Constants;

namespace Goldix.Application.Validators.WalletManagement;

public class UpdateWalletIncreaseRequestStatusDtoValidator : AbstractValidator<UpdateWalletIncreaseRequestStatusDto>
{
    public UpdateWalletIncreaseRequestStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.AdminId)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .WithName("شناسه ادمین");
    }
}
