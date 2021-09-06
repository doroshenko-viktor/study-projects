using MediatR;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Dto.Commands;

public record UpdatePostCommand : IRequest<PostEntity>
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Body { get; init; }
}