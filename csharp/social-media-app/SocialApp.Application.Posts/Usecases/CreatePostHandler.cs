using MediatR;
using SocialApp.Application.Posts.Dto.Commands;
using SocialApp.Application.Posts.Mapping;
using SocialApp.Domain.Common.Exceptions;
using SocialApp.Domain.Posts.Entities;
using SocialApp.Domain.Posts.Interfaces.Repositories;
using SocialApp.Domain.Posts.Interfaces.Validators;

namespace SocialApp.Application.Posts.Usecases;

public class CreatePostHandler : IRequestHandler<CreatePostCommand, PostEntity>
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

    public async Task<PostEntity> Handle(
        CreatePostCommand request,
        CancellationToken ct
    )
    {
        await Validate(request, ct);

        var postEntity = request.ToEntity();
        await _repository.CreatePost(postEntity);

        return postEntity;
    }

    private async Task Validate(CreatePostCommand request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult, "Error validating new post");
        }
    }
}