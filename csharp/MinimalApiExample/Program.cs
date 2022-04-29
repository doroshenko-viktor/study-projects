using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApiExample.Dto;
using MinimalApiExample.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/todos", async (
    [FromBody] Todo todo,
    [FromServices] TodoContext context) =>
{
    context.Todos.Add(todo);
    await context.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapGet("/todos/{id}", async (
    [FromRoute] int id,
    [FromServices] TodoContext context) =>
{
    var todo = await context.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound($"Todo with Id: {id} not found");
    return Results.Ok(todo);
});

app.MapGet("/todos", async ([FromServices] TodoContext context) =>
{
    var todos = context.Todos;
    return Results.Ok(todos);
});

app.MapGet("/todos/completed", async ([FromServices] TodoContext context) =>
{
    var todos = context.Todos.Where(x => x.IsCompleted);
    return Results.Ok(todos);
});

app.MapGet("/todos/uncompleted", async ([FromServices] TodoContext context) =>
{
    var todos = context.Todos.Where(x => !x.IsCompleted);
    return Results.Ok(todos);
});

app.MapPut("/todos/{id}/complete", async (
    [FromRoute] int id,
    [FromServices] TodoContext context) =>
{
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
    var todo = await context.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound($"Todo {id} not found");
    context.Todos.Remove(todo);
    await context.SaveChangesAsync();
    return Results.Ok($"Todo with id {id} deleted");
});

app.Run();
