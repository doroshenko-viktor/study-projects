using SocialApp.Domain.Common.Exceptions;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Infrastructure.Mongo.Posts.DAO;

namespace SocialApp.Infrastructure.Mongo.Factories;

public static class PostFactory
{
    public static PostEntity ToPostEntity(this PostMongoDAO postDao)
    {
        return PostEntity.Recreate(
            id: postDao.Id ?? throw new ValidationException("post id is null") { IsServerSide = true },
            title: postDao.Title ?? throw new ValidationException("post title is null") { IsServerSide = true },
            body: postDao.Body ?? throw new ValidationException("post body is null") { IsServerSide = true }
        );
    }
}