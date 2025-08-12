using Goldix.Application.Exceptions;
using Goldix.Application.Wrappers;

namespace Goldix.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == 401 || context.Response.StatusCode == 403)
                await HandleResponseAsync(context);
        }
        catch (Exception ex)
        {
            await HandleResponseAsync(context, _logger, ex);
        }
    }

    private static async Task HandleResponseAsync(HttpContext context, Microsoft.Extensions.Logging.ILogger logger = null, Exception exception = null)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.ContentType = "application/json";
        ApiResponse<object> response = null;

        if (exception != null)
        {
            switch (exception)
            {
                case CustomValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ExceptionResponse.CreateValidationResponse(validationEx);
                    break;
                case Exception badRequestEx when badRequestEx is BadRequestException or BadHttpRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ExceptionResponse.CreateBadRequestResponse(badRequestEx);
                    break;
                case NotFoundException notFoundEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = ExceptionResponse.CreateNotFoundResponse(notFoundEx);
                    break;
                case JsonException or { InnerException: JsonException }:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ExceptionResponse.CreateJsonExceptionResponse(exception);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ExceptionResponse.CreateUnhandledErrorResponse(exception, logger);
                    break;
            }
        }
        else
        {
            switch (context.Response.StatusCode)
            {
                case 401:
                    response = ExceptionResponse.CreateUnauthorizedResponse();
                    break;
                case 403:
                    response = ExceptionResponse.CreateForbiddenResponse();
                    break;
                default:
                    return;
            }
        }

        if (response is not null)
            await context.Response.WriteAsJsonAsync(response);
    }
}