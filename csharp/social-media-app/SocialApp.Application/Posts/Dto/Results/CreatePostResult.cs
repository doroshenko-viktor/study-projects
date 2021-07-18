using FluentValidation.Results;
using SocialApp.Domain.Posts.Entities;

namespace SocialApp.Application.Posts.Dto.Results;

public record CreatePostResult(
    bool Success = true,
    ValidationResult? ValidationResult = null,
    PostEntity? Post = null
);
