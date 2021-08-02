using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Domain.Posts.Interfaces.Repositories;

public interface IPostsRepository
{
    Task CreatePost(PostEntity post);
}