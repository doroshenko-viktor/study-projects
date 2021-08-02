using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SocialApp.Application.Posts.Usecases;
using SocialApp.Application.Posts.Validators;
using SocialApp.Domain.Posts.Configuration;
using SocialApp.Domain.Posts.Interfaces.Validators;

namespace SocialApp.Application.Configuration;

public static class ApplicationConfigurator
{
    public static IServiceCollection SetupPostsApplication(this IServiceCollection services)
    {
        services.SetupPosts();
        services.AddSingleton<ICreatePostValidator, CreatePostValidator>();
        services.AddMediatR(typeof(CreatePostHandler).Assembly);

        return services;
    }
}