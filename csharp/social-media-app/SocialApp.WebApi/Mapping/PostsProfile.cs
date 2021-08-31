using AutoMapper;
using SocialApp.Application.Posts.Dto.Commands;
using SocialApp.Contracts.Posts;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.WebApi.Mapping;

public class PostsProfile : Profile
{
    public PostsProfile()
    {
        CreateMap<CreatePostDto, CreatePostCommand>();
        CreateMap<UpdatePostCommand, PostDto>();
        CreateMap<PostEntity, PostDto>().ReverseMap();
    }
}