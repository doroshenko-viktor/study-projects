using MediatR;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Dto.Queries;

public record GetPostByIdQuery : IRequest<PostEntity>
{
    public GetPostByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}