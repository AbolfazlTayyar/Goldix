using Goldix.Application.Models.Product;
using Goldix.Domain.Constants;

namespace Goldix.Application.Validators.Product;

public class CreateMeasurementUnitDtoValidator : AbstractValidator<MeasurementUnitDto>
{
    public CreateMeasurementUnitDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DataSchemaConstants.DEFAULT_STRING_LENGTH)
            .WithName("نام");
    }
}
