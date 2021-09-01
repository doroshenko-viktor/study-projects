using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialApp.Application.Posts.Interfaces.Factories;
using SocialApp.Infrastructure.Mongo.Factories;
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

        services.AddScoped<IPostsRepositoryFactory>(sp =>
        {
            var dbClient = sp.GetRequiredService<IMongoClient>();
            return new PostsRepositoryFactory(dbClient);
        });

        return services;
    }
}