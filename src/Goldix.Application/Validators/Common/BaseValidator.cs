namespace Goldix.Application.Validators.Common;

public class BaseValidator<T> : AbstractValidator<T>
{
    public BaseValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}
