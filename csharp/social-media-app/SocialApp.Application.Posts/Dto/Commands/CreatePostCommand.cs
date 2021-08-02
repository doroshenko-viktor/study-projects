using MediatR;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Dto.Commands;

public record CreatePostCommand : IRequest<PostEntity>
{
    public string? Title { get; init; }
    public string? Body { get; init; }
}
