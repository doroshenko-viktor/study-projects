namespace SocialApp.Application.Exceptions;

public class MappingException : Exception
{
    private MappingException() : base()
    {
    }

    public MappingException(string message) : base(message)
    {
    }

    private MappingException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}