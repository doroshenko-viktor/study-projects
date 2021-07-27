namespace SocialApp.Infrastructure.Mongo.Configuration;

public record MongoDbOptions
{
    public string? ConnectionString { get; init; }
}