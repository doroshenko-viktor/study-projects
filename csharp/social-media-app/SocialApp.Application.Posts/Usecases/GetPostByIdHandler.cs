using MediatR;
using SocialApp.Application.Posts.Dto.Requests;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Domain.Posts.Interfaces.Repositories;

namespace SocialApp.Application.Posts.Usecases;

public class GetPostByIdHandler : IRequestHandler<GetPostByIdQuery, PostEntity>
{
    private readonly IPostsRepository _repository;

    public GetPostByIdHandler(IPostsRepository repository)
    {
        _repository = repository;
    }

    public Task<PostEntity> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = _repository.Get(request.Id);
        return post;
    }
}