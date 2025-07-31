namespace Goldix.Application.Interfaces.Infrastructure;

public interface IValidationService : IScopedService
{
    Task ValidateAsync<T>(T entity, CancellationToken cancellationToken = default);
}