using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Domain.Posts.Interfaces.Repositories;

public interface IPostsRepository
{
    Task Insert(PostEntity post);
    Task<PostEntity> Get(Guid id);
    IAsyncEnumerable<PostEntity> Get();
}