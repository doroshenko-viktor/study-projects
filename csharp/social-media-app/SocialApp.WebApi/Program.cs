using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.Commands;
using SocialApp.Application.Configuration;
using SocialApp.Infrastructure.Mongo.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupApplication();
builder.Services.SetupRepositories(options => builder.Configuration.GetSection("MongoSettings").Bind(options));

var app = builder.Build();

app.MapPost("/api/v1/posts", async (
    [FromServices] IMediator mediator,
    [FromBody] CreatePostCommand post
) =>
{
    var result = await mediator.Send(post);
    if (result.Success)
    {
        return Results.Ok(result.Post);
    }
    if (result.ValidationResult is not null)
    {
        return Results.BadRequest(result.ValidationResult);
    }
    return Results.BadRequest();
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
