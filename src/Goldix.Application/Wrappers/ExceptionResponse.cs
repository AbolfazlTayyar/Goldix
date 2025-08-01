using Goldix.Application.Exceptions;

namespace Goldix.Application.Wrappers;

public static class ExceptionResponse
{
    public static ApiResponse<object> CreateValidationResponse(CustomValidationException exception)
    {
        var response = ApiResponse.Fail(exception.Errors.Select(e => e.ErrorMessage).ToList());
        return response;
    }

    public static ApiResponse<object> CreateBadRequestResponse(BadRequestException exception)
    {
        var response = ApiResponse.Fail(exception.Message);
        return response;
    }

    public static ApiResponse<object> CreateNotFoundResponse(NotFoundException exception)
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

    public static ApiResponse<object> CreateUnhandledErrorResponse(Exception exception)
    {
        ///production
        //return ApiResponse<object>.FailureResult(
        //    "An unhandled exception occurred");

        ///development
        //return ApiResponse<object>.FailureResult(
        //   exception?.InnerException.ToString() ?? exception.ToString());

        var response = ApiResponse.Fail(exception.InnerException != null ? exception.InnerException.ToString() : exception.ToString());
        response.Message = "An unhandled exception occurred";
        return response;
    }
}
