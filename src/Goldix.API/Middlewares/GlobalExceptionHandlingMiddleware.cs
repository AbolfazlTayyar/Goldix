using Goldix.Application.Exceptions;
using Goldix.Application.Wrappers;

namespace Goldix.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger, IHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
        _next = next;
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
            await HandleResponseAsync(context, _logger, ex, _environment);
        }
    }

    private static async Task HandleResponseAsync(HttpContext context, ILogger logger = null, Exception exception = null, IHostEnvironment environment = null)
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
                    response = ExceptionResponse.CreateUnhandledErrorResponse(exception, logger, environment);
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