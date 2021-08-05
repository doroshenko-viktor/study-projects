namespace SocialApp.Domain.Posts.Entities;

public sealed class PostEntity
{
    private PostEntity(Guid id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }

    public static PostEntity CreateNew(string title, string body) => new(
        id: Guid.NewGuid(),
        title: title,
        body: body
    );

    public static PostEntity Recreate(Guid id, string title, string body) => new(
        id: id,
        title: title,
        body: body
    );

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
}