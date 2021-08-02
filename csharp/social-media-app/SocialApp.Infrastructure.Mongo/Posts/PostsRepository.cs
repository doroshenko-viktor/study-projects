using MongoDB.Driver;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Domain.Posts.Interfaces.Repositories;
using SocialApp.Infrastructure.Mongo.Posts.DAO;
using SocialApp.Infrastructure.Mongo.Posts.Mapping;

namespace SocialApp.Infrastructure.Mongo.Posts;

public class PostsRepository : IPostsRepository
{
    private readonly IMongoCollection<PostMongoDAO> _collection;

    public PostsRepository(IMongoCollection<PostMongoDAO> collection)
    {
        _collection = collection;
    }

    public async Task CreatePost(PostEntity post)
    {
        var postDAO = post.ToDAO();
        await _collection.InsertOneAsync(postDAO);
    }
}