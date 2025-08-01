using Goldix.Application.Models.User.Register;
using Goldix.Domain.Constants;

namespace Goldix.Application.Validators.User;

public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .WithName("نام");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .WithName("نام خانوادگی");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_PHONE_NUMBER_LENGTH)
            .WithName("شماره موبایل");

        RuleFor(x => x.RePassword)
            .Equal(x => x.Password)
            .WithMessage(".رمز عبور و تکرار آن باید یکسان باشند")
            .WithName("تکرار رمز عبور");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(DataSchemaConstants.DEFAULT_PASSWORD_LENGTH)
            .WithMessage(".رمز عبور باید حداقل ۸ کاراکتر باشد")
            .Matches(@"[0-9]")
            .WithMessage(".رمز عبور باید حداقل شامل یک عدد باشد")
            .Matches(@"[a-z]")
            .WithMessage(".رمز عبور باید حداقل شامل یک حرف کوچک انگلیسی باشد")
            .Matches(@"[A-Z]")
            .WithMessage(".رمز عبور باید حداقل شامل یک حرف بزرگ انگلیسی باشد")
            .Matches(@"[^a-zA-Z0-9]")
            .WithMessage(".رمز عبور باید حداقل شامل یک علامت خاص (مثل @، #، ! و...) باشد")
            .WithName("رمز عبور");
    }
}
