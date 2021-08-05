namespace SocialApp.Contracts.Posts;

public record struct PostDto
{
    public Guid? Id { get; init; }
    public string? Title { get; init; }
    public string? Body { get; init; }
}