using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SocialApp.Application.Posts.Interfaces.Validators;
using SocialApp.Application.Posts.Usecases;
using SocialApp.Application.Posts.Validators;

namespace SocialApp.Application.Configuration;

public static class ApplicationConfigurator
{
    public static IServiceCollection SetupApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICreatePostValidator, CreatePostValidator>();
        services.AddMediatR(typeof(CreatePostHandler).Assembly);

        return services;
    }
}