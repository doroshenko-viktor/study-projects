using FluentValidation;
using SocialApp.Application.Commands;

namespace SocialApp.Application.Posts.Interfaces.Validators;

public interface ICreatePostValidator : IValidator<CreatePostCommand> { }