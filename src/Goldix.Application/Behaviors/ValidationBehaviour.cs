using Goldix.Application.Interfaces.Validator;

namespace Goldix.Application.Behaviors;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IValidationService _validationService;

    public ValidationBehaviour(IValidationService validationService)
    {
        _validationService = validationService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await _validationService.ValidateAsync(request, cancellationToken);
        return await next();
    }
}