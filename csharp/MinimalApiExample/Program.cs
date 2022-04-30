using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiExample.Dto;
using MinimalApiExample.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();
app.UseHttpLogging();
var logger = app.Logger;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/todos", async (
    [FromBody] Todo todo,
    [FromServices] TodoContext context) =>
{
    logger.LogInformation("Creating Todo {Name}", todo.Name);
    context.Todos.Add(todo);
    await context.SaveChangesAsync();
    logger.LogInformation("Todo {@Todo} created", todo);
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapGet("/todos/{id}", async (
    [FromRoute] int id,
    [FromServices] TodoContext context) =>
{
    logger.LogInformation("Trying to retrieve Todo with id: {Id}", id);
    var todo = await context.Todos.FindAsync(id);

    if (todo is null)
    {
        logger.LogInformation("Todo {Id} has not been found", id);
        return Results.NotFound($"Todo with Id: {id} not found");
    }

    logger.LogInformation("Todo {Id} found", id);
    return Results.Ok(todo);
});

app.MapGet("/todos", ([FromServices] TodoContext context) =>
{
    logger.LogInformation("Retrieving all existing todos");
    var todos = context.Todos;
    logger.LogInformation("Existing Todos retrieved");
    return Results.Ok(todos);
});

app.MapGet("/todos/completed", async ([FromServices] TodoContext context) =>
{
    logger.LogInformation("Retrieving completed Todos");
    var todos = context.Todos.Where(x => x.IsCompleted);
    logger.LogInformation("Completed Todos retrieved");
    return Results.Ok(todos);
});

app.MapGet("/todos/uncompleted", async ([FromServices] TodoContext context) =>
{
    logger.LogInformation("Retrieving not completed Todos");
    var todos = context.Todos.Where(x => !x.IsCompleted);
    logger.LogInformation("Not completed Todos are retrieved");
    return Results.Ok(todos);
});

app.MapPut("/todos/{id}/complete", async (
    [FromRoute] int id,
    [FromServices] TodoContext context) =>
{
    logger.LogInformation("Trying to complete Todo {Id}", id);
    var todo = await context.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound($"Todo with Id: {id} not found");
    if (todo.IsCompleted) return Results.Conflict($"Can't perform: Todo {id} already completed");

    todo.IsCompleted = true;
    await context.SaveChangesAsync();

    return Results.Ok($"Todo {id} completed");
});

app.MapDelete("/todos/{id}", async (
    [FromRoute] int id,
    [FromServices] TodoContext context) =>
{
    logger.LogInformation("Trying to delete Todo {Id}", id);
    var todo = await context.Todos.FindAsync(id);
    if (todo is null)
    {
        logger.LogWarning("Todo {id} has not been found", id);
        return Results.NotFound($"Todo {id} not found");
    }
    context.Todos.Remove(todo);
    await context.SaveChangesAsync();
    logger.LogInformation("Todo {Id} has been removed", id);

    return Results.Ok($"Todo with id {id} deleted");
});

app.Urls.Add("http://localhost:3001");

app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "3002"}");
