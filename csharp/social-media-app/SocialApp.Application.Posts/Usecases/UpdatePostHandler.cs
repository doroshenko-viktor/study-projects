using MediatR;
using SocialApp.Application.Posts.Dto.Commands;
using SocialApp.Application.Posts.Interfaces.Factories;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Domain.Posts.Interfaces.Repositories;

namespace SocialApp.Application.Posts.Usecases;

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, PostEntity>
{
    private readonly IPostsRepositoryFactory _repositoryFactory;

    public UpdatePostHandler(IPostsRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }

    public async Task<PostEntity> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var repository = await _repositoryFactory.Get();
        return await UpdatePost(request, repository);
    }

    private static async Task<PostEntity> UpdatePost(UpdatePostCommand request, IPostsRepository repository)
    {
        try
        {
            repository.StartTransaction();
            var post = await repository.Get(request.Id);

            if (request.Title is not null) post.UpdateTitle(request.Title);
            if (request.Body is not null) post.UpdateBody(request.Body);

            await repository.Update(post);
            await repository.CommitTransactionAsync();

            return post;
        }
        catch (Exception)
        {
            await repository.AbortTransactionAsync();
            throw;
        }
    }
}
