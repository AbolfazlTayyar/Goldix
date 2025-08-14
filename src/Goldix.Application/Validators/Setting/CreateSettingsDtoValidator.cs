using Goldix.Application.Models.Setting;

namespace Goldix.Application.Validators.Settings;

public class CreateSettingsDtoValidator : AbstractValidator<SettingsDto>
{
    public CreateSettingsDtoValidator()
    {
        RuleFor(x => x.SmsApiKey)
            .NotEmpty()
            .WithName("کد دسترسی");
    }
}
