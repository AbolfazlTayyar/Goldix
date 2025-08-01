using Goldix.Application.Models.Setting;

namespace Goldix.Application.Validators.Settings;

public class CreateSettingsDtoValidator : AbstractValidator<CreateUpdateSettingsDto>
{
    public CreateSettingsDtoValidator()
    {
        RuleFor(x => x.SmsApiKey)
            .NotEmpty()
            .WithName("کد دسترسی");
    }
}
