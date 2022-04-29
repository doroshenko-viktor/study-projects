using Microsoft.EntityFrameworkCore;
using MinimalApiExample.Dto;

namespace MinimalApiExample.Persistence;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> contextOptions) : base(contextOptions) { }

    public DbSet<Todo> Todos => Set<Todo>();
}