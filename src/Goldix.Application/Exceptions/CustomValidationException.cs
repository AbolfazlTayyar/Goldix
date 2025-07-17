using FluentValidation.Results;

namespace Goldix.Application.Exceptions;

public class CustomValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; }

    public CustomValidationException(IEnumerable<ValidationFailure> errors)
        : base("One or more validation failures have occurred.")
    {
        Errors = errors;
    }
}