using Goldix.Application.Models.User.GetToken;
using Goldix.Domain.Constants;

namespace Goldix.Application.Validators.User;

public class GetTokenRequestDtoValidator : AbstractValidator<GetTokenRequestDto>
{
    public GetTokenRequestDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_STRING_LENGTH)
            .WithName("نام کاربری");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(DataSchemaConstants.DEFAULT_PASSWORD_LENGTH)
            .WithName("رمز عبور");
    }
}
