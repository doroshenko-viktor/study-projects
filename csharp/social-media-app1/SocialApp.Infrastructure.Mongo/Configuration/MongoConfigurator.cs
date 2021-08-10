using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialApp.Domain.Posts.Interfaces.Repositories;
using SocialApp.Infrastructure.Mongo.Posts;
using SocialApp.Infrastructure.Mongo.Posts.DAO;

namespace SocialApp.Infrastructure.Mongo.Configuration;

public static class MongoConfigurator
{
    public static IServiceCollection SetupRepositories(
        this IServiceCollection services,
        Action<MongoDbOptions> configureOptions
    )
    {
        services.Configure(configureOptions);

        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoDbOptions>>();
            return new MongoClient(options.Value.ConnectionString);
        });

        services.AddSingleton<IMongoCollection<PostMongoDAO>>(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            return mongoClient.GetDatabase("social-app").GetCollection<PostMongoDAO>("posts");
        });

        services.AddScoped<IPostsRepository, PostsRepository>();

        return services;
    }
}