using MongoDB.Driver;
using SocialApp.Domain.Common.Exceptions;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Domain.Posts.Interfaces.Repositories;
using SocialApp.Infrastructure.Mongo.Factories;
using SocialApp.Infrastructure.Mongo.Posts.DAO;
using SocialApp.Infrastructure.Mongo.Posts.Mapping;

namespace SocialApp.Infrastructure.Mongo.Posts;

public class PostsRepository : IPostsRepository, IDisposable
{
    private readonly IMongoCollection<PostMongoDAO> _collection;
    private readonly IClientSessionHandle _session;

    public PostsRepository(IMongoCollection<PostMongoDAO> collection, IClientSessionHandle session)
    {
        _collection = collection;
        _session = session;
    }

    public void StartTransaction()
    {
        _session.StartTransaction();
    }

    public Task AbortTransactionAsync()
    {
        return _session.AbortTransactionAsync();
    }

    public Task CommitTransactionAsync()
    {
        return _session.CommitTransactionAsync();
    }

    public void Dispose()
    {
        _session.AbortTransaction();
        _session.Dispose();
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

    public async Task Update(PostEntity post)
    {
        await _collection.FindOneAndUpdateAsync(x => x.Id == post.Id,
            Builders<PostMongoDAO>.Update
                .Set(x => x.Body, post.Body)
                .Set(x => x.Title, post.Title)
        );
    }
}