using Goldix.Application.Exceptions;

namespace Goldix.Application.Wrappers;

public static class ExceptionResponse
{
    public static ApiResponse<object> CreateValidationResponse(CustomValidationException exception)
    {
        return ApiResponse<object>.FailureResult(
            exception.Errors.Select(e => e.ErrorMessage),
            "Validation failed");
    }

    public static ApiResponse<object> CreateBadRequestResponse(BadRequestException exception)
    {
        return ApiResponse<object>.FailureResult(
            exception.Message,
            "Bad request");
    }

    public static ApiResponse<object> CreateNotFoundResponse(NotFoundException exception)
    {
        return ApiResponse<object>.FailureResult(
            exception.Message,
            "Resource not found");
    }

    public static ApiResponse<object> CreateUnhandledErrorResponse(Exception exception)
    {
        ///production
        //return ApiResponse<object>.FailureResult(
        //    "An unhandled exception occurred");

        ///development
        //return ApiResponse<object>.FailureResult(
        //   exception?.InnerException.ToString() ?? exception.ToString());
        return ApiResponse<object>.FailureResult(
           exception.InnerException != null ? exception.InnerException.ToString() : exception.ToString());
    }
}
