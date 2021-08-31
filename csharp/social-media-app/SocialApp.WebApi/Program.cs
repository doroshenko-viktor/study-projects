using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.Configuration;
using SocialApp.Application.Posts.Dto.Commands;
using SocialApp.Application.Posts.Dto.Queries;
using SocialApp.Contracts.Posts;
using SocialApp.Domain.Common.Exceptions;
using SocialApp.Infrastructure.Mongo.Configuration;
using SocialApp.WebApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupPostsApplication();
builder.Services.SetupRepositories(options => builder.Configuration.GetSection("MongoSettings").Bind(options));
builder.Services.AddAutoMapper(mapperConfiguration => mapperConfiguration.AddProfile<PostsProfile>());

var app = builder.Build();

var mapper = app.Services.GetRequiredService<IMapper>();

app.MapPost("/api/v1/posts", async ([FromServices] IMediator mediator, [FromBody] CreatePostDto post) =>
{
    try
    {
        var command = mapper.Map<CreatePostCommand>(post);
        var result = await mediator.Send(command);
        var resultDto = mapper.Map<PostDto>(result);
        return Results.Ok(resultDto);
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

app.MapGet("/api/v1/posts/{id}", async ([FromServices] IMediator mediator, [FromRoute] Guid id) =>
{
    try
    {
        var command = new GetPostByIdQuery(id);
        var result = await mediator.Send(command);
        var resultDto = mapper.Map<PostDto>(result);
        return Results.Ok(resultDto);
    }
    catch (EntityNotFoundException)
    {
        return Results.NotFound();
    }
    catch (ValidationException e) when (e.IsServerSide)
    {
        return Results.UnprocessableEntity(e);
    }
});

app.MapGet("/api/v1/posts", async ([FromServices] IMediator mediator) =>
{
    var command = new GetAllPostsQuery();
    var res = mediator.CreateStream(command);
    return Results.Ok(res);
});

app.MapPut("/api/posts/{id}", async ([FromServices] IMediator mediator, [FromRoute] Guid id, [FromBody] PostDto post) =>
{
    try
    {
        var result = await mediator.Send(mapper.Map<UpdatePostCommand>(post) with { Id = id });
        return Results.Ok(result);
    }
    catch (EntityNotFoundException)
    {
        return Results.NotFound();
    }
    catch (ValidationException e)
    {
        return Results.BadRequest(e.Message);
    }
    catch (Exception e)
    {
        return Results.Problem(e.Message);
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
