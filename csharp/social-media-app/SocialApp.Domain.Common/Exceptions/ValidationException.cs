using FluentValidation.Results;

namespace SocialApp.Domain.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationResult? ValidationResult { get; }
    public bool IsServerSide { get; init; } = false;

    private ValidationException() : base()
    {
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(ValidationResult validationResult, string? message) : base(message)
    {
        ValidationResult = validationResult;
    }

    private ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}