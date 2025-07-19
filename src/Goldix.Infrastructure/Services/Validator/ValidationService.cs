using Goldix.Application.Exceptions;
using Goldix.Application.Interfaces.Validator;

namespace Goldix.Infrastructure.Services.Validator;

public class ValidationService(IServiceProvider serviceProvider) : IValidationService
{
    public async Task ValidateAsync<T>(T entity, CancellationToken cancellationToken)
    {
        var validator = serviceProvider.GetService<IValidator<T>>();
        if (validator != null)
        {
            var validationContext = new ValidationContext<T>(entity);
            var validationResult = await validator.ValidateAsync(validationContext, cancellationToken);

            if (!validationResult.IsValid)
                throw new CustomValidationException(validationResult.Errors);
        }
    }
}