namespace SocialApp.Domain.Posts.Entities;

public sealed class PostEntity
{
    private PostEntity(Guid id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }

    public static PostEntity Create(string title, string body)
    {
        return new PostEntity(
            id: Guid.NewGuid(),
            title: title,
            body: body
        );
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }
}