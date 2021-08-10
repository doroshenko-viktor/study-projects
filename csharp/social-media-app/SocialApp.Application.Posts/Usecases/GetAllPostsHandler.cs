using MediatR;
using SocialApp.Application.Posts.Dto.Queries;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Domain.Posts.Interfaces.Repositories;

namespace SocialApp.Application.Posts.Usecases;

public class GetAllPostsHandler : IStreamRequestHandler<GetAllPostsQuery, PostEntity>
{
    private readonly IPostsRepository _repository;

    public GetAllPostsHandler(IPostsRepository repository)
    {
        _repository = repository;
    }

    public IAsyncEnumerable<PostEntity> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = _repository.Get();
        return posts;
    }
}
