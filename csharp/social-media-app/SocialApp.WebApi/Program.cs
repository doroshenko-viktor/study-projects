using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.Configuration;
using SocialApp.Application.Posts.Dto.Commands;
using SocialApp.Domain.Common.Exceptions;
using SocialApp.Infrastructure.Mongo.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupPostsApplication();
builder.Services.SetupRepositories(options => builder.Configuration.GetSection("MongoSettings").Bind(options));

var app = builder.Build();

app.MapPost("/api/v1/posts", async (
    [FromServices] IMediator mediator,
    [FromBody] CreatePostCommand post
) =>
{
    try
    {
        var result = await mediator.Send(post);
        return Results.Ok(result);
    }
    catch (ValidationException e)
    {
        if (e.ValidationResult is not null)
        {
            return Results.BadRequest(e.ValidationResult);
        }

        return Results.BadRequest(e.Message);
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
