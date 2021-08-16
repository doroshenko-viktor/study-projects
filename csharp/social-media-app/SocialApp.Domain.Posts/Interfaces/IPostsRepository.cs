using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Domain.Posts.Interfaces.Repositories;

public interface IPostsRepository
{
    void StartTransaction();
    Task CommitTransactionAsync();
    Task AbortTransactionAsync();
    // todo: add cancellation token
    Task Insert(PostEntity post);
    Task<PostEntity> Get(Guid id);
    IAsyncEnumerable<PostEntity> Get();
    Task Update(PostEntity post);
}