using Goldix.Application.Interfaces.Infrastructure;

namespace Goldix.API.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
    private readonly IValidationService _validationService;

    public ValidationFilter(IValidationService validationService)
    {
        _validationService = validationService;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        T argToValidate = context.GetArgument<T>(0);
        await _validationService.ValidateAsync(argToValidate);
        return await next.Invoke(context);
    }
}
