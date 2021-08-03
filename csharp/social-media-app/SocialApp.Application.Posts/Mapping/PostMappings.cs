using SocialApp.Application.Exceptions;
using SocialApp.Application.Posts.Dto.Commands;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Mapping;

public static class PostMappings
{
    public static PostEntity ToEntity(this CreatePostCommand command)
    {
        return PostEntity.CreateNew(
            title: command?.Title ?? throw new MappingException("CreatePostCommand.Title is null"),
            body: command?.Body ?? throw new MappingException("CreatePostCommand.Body is null")
        );
    }
}