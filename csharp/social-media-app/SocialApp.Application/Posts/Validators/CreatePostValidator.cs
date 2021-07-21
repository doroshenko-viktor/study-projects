using FluentValidation;
using SocialApp.Application.Commands;
using SocialApp.Application.Posts.Interfaces.Validators;

namespace SocialApp.Application.Posts.Validators;

public class CreatePostValidator : AbstractValidator<CreatePostCommand>, ICreatePostValidator
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Body).NotEmpty();
    }
}