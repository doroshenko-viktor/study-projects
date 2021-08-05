using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace SocialApp.Infrastructure.Mongo.Posts.DAO;

public record PostMongoDAO
{
    [BsonId(IdGenerator = typeof(GuidGenerator)), BsonRepresentation(BsonType.String)]
    public Guid? Id { get; init; }
    public string? Title { get; init; }
    public string? Body { get; init; }
}