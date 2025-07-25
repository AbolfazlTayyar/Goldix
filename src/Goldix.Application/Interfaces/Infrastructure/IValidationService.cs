namespace Goldix.Application.Interfaces.Infrastructure;

public interface IValidationService
{
    Task ValidateAsync<T>(T entity, CancellationToken cancellationToken = default);
}