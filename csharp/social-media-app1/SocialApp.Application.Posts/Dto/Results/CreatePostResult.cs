using SocialApp.Application.Common;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Dto.Results;

public class CreatePostResult : BaseUsecaseResult<PostEntity>
{
    public CreatePostResult(PostEntity body) : base(body)
    {
    }
}