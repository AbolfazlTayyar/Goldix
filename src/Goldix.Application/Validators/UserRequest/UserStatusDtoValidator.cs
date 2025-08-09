using Goldix.Application.Models.UserRequest;

namespace Goldix.Application.Validators.UserRequest;

public class UserStatusDtoValidator : AbstractValidator<UserStatusDto>
{
    public UserStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
