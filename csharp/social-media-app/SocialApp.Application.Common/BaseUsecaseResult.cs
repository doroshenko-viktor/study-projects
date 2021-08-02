namespace SocialApp.Application.Common;

public abstract class BaseUsecaseResult<T>
{
    protected BaseUsecaseResult(T body)
    {
        Body = body;
    }

    public T Body { get; }
}