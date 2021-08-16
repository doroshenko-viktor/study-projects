using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Domain.Posts.Interfaces;

public interface IPostsService
{
    public void UpdatePost(PostEntity post);
}