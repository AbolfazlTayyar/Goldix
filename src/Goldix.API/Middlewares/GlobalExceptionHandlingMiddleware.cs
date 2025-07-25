using Goldix.Application.Exceptions;
using Goldix.Application.Wrappers;

namespace Goldix.API.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ApiResponse<object> response = null;
        switch (exception)
        {
            case CustomValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = ExceptionResponse.CreateValidationResponse(validationEx);
                break;

            case BadRequestException badRequestEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = ExceptionResponse.CreateBadRequestResponse(badRequestEx);
                break;

            case NotFoundException notFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = ExceptionResponse.CreateNotFoundResponse(notFoundEx);
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = ExceptionResponse.CreateUnhandledErrorResponse(exception);
                break;
        }

        await context.Response.WriteAsJsonAsync(response);
    }
}