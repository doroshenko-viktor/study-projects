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

    public async Task<PostEntity> Get(Guid id)
    {
        PostMongoDAO? post = await (await _collection.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();

        if (post is null)
        {
            throw new EntityNotFoundException(nameof(PostEntity), id.ToString());
        }

        // todo: create validator
        return post.ToPostEntity();
    }

    public async IAsyncEnumerable<PostEntity> Get()
    {
        var y = new FindOptions<PostMongoDAO>();
        var x = _collection.FindSync(_ => true, y);

        // todo: abstract
        while (await x.MoveNextAsync())
        {
            foreach (var x1 in x.Current)
            {
                yield return x1.ToPostEntity();
            }
        }
    }

    public async Task Insert(PostEntity post)
    {
        var postDAO = post.ToDAO();
        await _collection.InsertOneAsync(postDAO);
    }
}