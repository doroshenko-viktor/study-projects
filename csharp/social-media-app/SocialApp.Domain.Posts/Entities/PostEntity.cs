namespace SocialApp.Domain.Posts.Entities;

public class PostEntity
{
    public PostEntity(Guid id, string title, string body)
    {
        Id = id;
        Title = title;
        Body = body;
    }

    public Guid Id { get; }
    public string Title { get; }
    public string Body { get; }
}