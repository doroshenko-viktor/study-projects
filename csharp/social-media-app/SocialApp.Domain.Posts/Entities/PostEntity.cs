using SocialApp.Domain.Common.Exceptions;

namespace SocialApp.Domain.Posts.Entities;

public sealed class PostEntity
{
    private PostEntity(Guid id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }

    public static PostEntity CreateNew(string title, string body)
    {
        return Create(Guid.NewGuid(), title, body);
    }

    public static PostEntity Create(Guid id, string title, string body)
    {
        ValidateTitle(title);
        ValidateBody(body);

        return new(
            id: id,
            title: title,
            body: body
        );
    }

    public Guid Id { get; }
    public string Title { get; private set; }
    public string Body { get; private set; }

    public void UpdateTitle(string newTitle)
    {
        ValidateTitle(newTitle);
        Title = newTitle;
    }

    public void UpdateBody(string newBody)
    {
        ValidateBody(newBody);
        Body = newBody;
    }

    private static void ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ValidationException(nameof(PostEntity), $"{nameof(Body)} can't be null or empty");
        }
    }

    private static void ValidateBody(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ValidationException(nameof(PostEntity), $"{nameof(Body)} can't be null or empty");
        }
    }
}