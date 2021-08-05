namespace SocialApp.Domain.Common.Exceptions;

public class EntityNotFoundException : Exception
{
    private EntityNotFoundException() : base()
    {
    }

    public EntityNotFoundException(string entityName, string entityIdentifier) : base($"{entityName}: {entityIdentifier} could not been found")
    {
    }

    private EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}