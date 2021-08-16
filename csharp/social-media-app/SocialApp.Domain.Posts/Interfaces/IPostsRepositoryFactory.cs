using SocialApp.Domain.Posts.Interfaces.Repositories;

namespace SocialApp.Application.Posts.Interfaces.Factories;

public interface IPostsRepositoryFactory
{
    Task<IPostsRepository> Get();
}