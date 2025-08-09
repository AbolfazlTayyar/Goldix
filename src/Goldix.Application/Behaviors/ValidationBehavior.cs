using Goldix.Application.Interfaces.Infrastructure;

namespace Goldix.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IValidationService _validationService;

    public ValidationBehavior(IValidationService validationService)
    {
        _validationService = validationService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await _validationService.ValidateAsync(request, cancellationToken);
        return await next();
    }
}