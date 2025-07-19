namespace Goldix.Application.Wrappers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; }

    // Success response with data
    public static ApiResponse<T> SuccessResult(T data, string message = "Operation successful")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            Errors = new List<string>()
        };
    }

    // Success response without data
    public static ApiResponse<T> SuccessResult(string message = "Operation successful")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = default,
            Errors = new List<string>()
        };
    }

    // Failure response
    public static ApiResponse<T> FailureResult(IEnumerable<string> errors, string message = "Operation failed")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default,
            Errors = errors
        };
    }

    // Single error failure response
    public static ApiResponse<T> FailureResult(string error, string message = "Operation failed")
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default,
            Errors = new List<string> { error }
        };
    }
}