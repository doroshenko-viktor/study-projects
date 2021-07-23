using MediatR;
using SocialApp.Application.Commands;
using SocialApp.Application.Posts.Dto.Results;
using SocialApp.Application.Posts.Interfaces.Repositories;
using SocialApp.Application.Posts.Interfaces.Validators;
using SocialApp.Application.Posts.Mapping;

namespace SocialApp.Application.Posts.Usecases;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, CreatePostResult>
{
    private readonly ICreatePostValidator _validator;
    private readonly IPostsRepository _repository;

    public CreatePostHandler(
        ICreatePostValidator validator,
        IPostsRepository repository
    )
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<CreatePostResult> Handle(
        CreatePostCommand request,
        CancellationToken ct
    )
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return new CreatePostResult(false, validationResult);
        }

        var postEntity = request.ToEntity();
        await _repository.CreatePost(postEntity);

        return new CreatePostResult { Post = postEntity }; // Create factory
    }
}