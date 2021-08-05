namespace SocialApp.Contracts.Posts;

public record struct CreatePostDto
{
    public string? Title { get; init; }
    public string? Body { get; init; }
}