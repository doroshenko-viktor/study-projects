using SocialApp.Application.Commands;
using SocialApp.Application.Exceptions;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Mapping;

public static class PostMappings
{
    public static PostEntity ToEntity(this CreatePostCommand command)
    {
        return PostEntity.Create(
            title: command?.Title ?? throw new MappingException("CreatePostCommand.Title is null"),
            body: command?.Body ?? throw new MappingException("CreatePostCommand.Body is null")
        );
    }
}