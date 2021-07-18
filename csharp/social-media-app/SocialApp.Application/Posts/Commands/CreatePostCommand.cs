using MediatR;
using SocialApp.Application.Posts.Dto.Results;

namespace SocialApp.Application.Commands;

public record CreatePostCommand : IRequest<CreatePostResult>
{
    public string? Title { get; init; }
    public string? Body { get; init; }
}
