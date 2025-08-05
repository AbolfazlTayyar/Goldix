namespace Goldix.Application.Validators.Configurations;

public class FluentValidationCustomLanguageManager : LanguageManager
{
    public FluentValidationCustomLanguageManager()
    {
        Culture = new CultureInfo("fa-IR");

        AddTranslation("fa-IR", "NotEmptyValidator", "{PropertyName} نمی‌ تواند خالی باشد");
        AddTranslation("fa-IR", "NotNullValidator", "{PropertyName} نمی‌تواند null باشد");
        AddTranslation("fa-IR", "MaximumLengthValidator", "{PropertyName} نباید بیشتر از {MaxLength} کاراکتر باشد");
    }
}
