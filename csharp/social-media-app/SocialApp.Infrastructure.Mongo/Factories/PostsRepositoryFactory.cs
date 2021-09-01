using MongoDB.Driver;
using SocialApp.Application.Posts.Interfaces.Factories;
using SocialApp.Domain.Posts.Interfaces.Repositories;
using SocialApp.Infrastructure.Mongo.Posts;
using SocialApp.Infrastructure.Mongo.Posts.DAO;

namespace SocialApp.Infrastructure.Mongo.Factories;

public class PostsRepositoryFactory : IPostsRepositoryFactory
{
    private readonly IMongoClient _dbClient;

    public PostsRepositoryFactory(IMongoClient dbClient)
    {
        _dbClient = dbClient;
    }

    public async Task<IPostsRepository> Get()
    {
        var collection = _dbClient
            .GetDatabase("social-app")
            .GetCollection<PostMongoDAO>("posts");

        var session = await _dbClient.StartSessionAsync();

        return new PostsRepository(collection, session);
    }
}