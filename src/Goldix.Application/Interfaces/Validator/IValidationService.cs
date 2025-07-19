namespace Goldix.Application.Interfaces.Validator;

public interface IValidationService
{
    Task ValidateAsync<T>(T entity, CancellationToken cancellationToken = default);
}