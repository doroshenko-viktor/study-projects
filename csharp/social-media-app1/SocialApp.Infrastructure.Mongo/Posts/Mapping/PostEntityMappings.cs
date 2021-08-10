using SocialApp.Domain.Posts.Entities;
using SocialApp.Infrastructure.Mongo.Posts.DAO;

namespace SocialApp.Infrastructure.Mongo.Posts.Mapping;

public static class PostEntityMappings
{
    public static PostMongoDAO ToDAO(this PostEntity post)
    {
        return new PostMongoDAO
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body
        };
    }
}