using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Interfaces.Repositories;

public interface IPostsRepository
{
    Task CreatePost(PostEntity post);
}