namespace Goldix.Application.Wrappers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ApiResponse<T> Ok(T data) => new() { Success = true, Data = data };
    public static ApiResponse<T> Ok(T data, string message) => new() { Success = true, Data = data, Message = message };
    public static ApiResponse<T> Fail(string error) => new() { Success = false, Errors = [error] };
    public static ApiResponse<T> Fail(List<string> errors) => new() { Success = false, Errors = errors };
}

public class ApiResponse : ApiResponse<object>
{
    public static new ApiResponse Ok() => new() { Success = true };
    public static new ApiResponse Ok(string message) => new() { Success = true, Message = message };
    public static new ApiResponse Fail(string error) => new() { Success = false, Errors = [error] };
    public static new ApiResponse Fail(List<string> errors) => new() { Success = false, Errors = errors };
}