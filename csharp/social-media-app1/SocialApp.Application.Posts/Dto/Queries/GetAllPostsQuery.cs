using MediatR;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Dto.Queries;

public class GetAllPostsQuery : IStreamRequest<PostEntity> { }