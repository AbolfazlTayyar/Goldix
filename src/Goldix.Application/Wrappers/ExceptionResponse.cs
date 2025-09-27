using Goldix.Application.Exceptions;
using Microsoft.Extensions.Hosting;

namespace Goldix.Application.Wrappers;

public static class ExceptionResponse
{
    public static ApiResponse<object> CreateValidationResponse(CustomValidationException exception)
    {
        var response = ApiResponse.Fail(exception.Errors.Select(e => e.ErrorMessage).ToList());
        return response;
    }

    public static ApiResponse<object> CreateBadRequestResponse(Exception exception)
    {
        var response = ApiResponse.Fail(exception.Message);
        return response;
    }

    public static ApiResponse<object> CreateNotFoundResponse(NotFoundException exception)
    {
        var response = ApiResponse.Fail(exception.Message);
        return response;
    }

    public static ApiResponse<object> CreateJsonExceptionResponse(Exception exception)
    {
        var response = ApiResponse.Fail(exception.Message);
        return response;
    }

    public static ApiResponse<object> CreateUnauthorizedResponse()
    {
        var response = ApiResponse.Fail("");
        return response;
    }

    public static ApiResponse<object> CreateForbiddenResponse()
    {
        var response = ApiResponse.Fail("");
        return response;
    }

    public static ApiResponse<object> CreateUnhandledErrorResponse(Exception exception, ILogger logger, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var response = ApiResponse.Fail(exception.InnerException?.ToString() ?? exception.ToString());
            response.Message = "An unhandled exception occurred";
            return response;
        }
        else
        {
            logger.LogError(exception: exception.InnerException, message: exception.Message);

            return ApiResponse<object>.Fail("An unhandled exception occurred");
        }
    }
}
