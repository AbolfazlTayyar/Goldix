using Goldix.Application.Models.Notification;
using Goldix.Domain.Constants;

namespace Goldix.Application.Validators.Notification;

public class NotificationContentDtoValidator : AbstractValidator<CreateNotificationDto>
{
    public NotificationContentDtoValidator()
    {
        RuleFor(x => x.SenderId)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_USER_ID_LENGTH)
            .WithName("شناسه کاربر فرستنده");

        RuleFor(x => x.Title)
            .MaximumLength(DataSchemaConstants.DEFAULT_STRING_LENGTH)
            .WithName("عنوان");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithName("شرح");
    }
}
