using Microsoft.Extensions.DependencyInjection;

namespace SocialApp.Domain.Posts.Configuration;

public static class PostsConfigurator
{
    public static IServiceCollection SetupPosts(this IServiceCollection services)
    {
        return services;
    }
}