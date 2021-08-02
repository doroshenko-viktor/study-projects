using FluentValidation;
using SocialApp.Application.Posts.Dto.Commands;

namespace SocialApp.Domain.Posts.Interfaces.Validators;

public interface ICreatePostValidator : IValidator<CreatePostCommand> { }