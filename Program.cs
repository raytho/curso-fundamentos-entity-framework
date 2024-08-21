using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;
using proyectoef.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(x => x.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("ConnectionTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconnection", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Data base in memory: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) => {
    return Results.Ok(dbContext.Tareas
            .Include(p => p.Categoria)
            .Where(p => p.PrioridadTarea == proyectoef.Models.Prioridad.Baja));
});

app.MapGet("/api/categorias", ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Categorias.Where(c => c.Peso >= 20));
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;

    await dbContext.AddAsync(tarea);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapPut("/api/tareas/{tareaId}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid tareaId) =>
{
    var currentTarea = dbContext.Tareas.Find(tareaId);
    
    if (currentTarea != null)
    {
        currentTarea.CategoriaId = tarea.CategoriaId;
        currentTarea.Titulo = tarea.Titulo;
        currentTarea.PrioridadTarea = tarea.PrioridadTarea;
        currentTarea.Descripcion = tarea.Descripcion;

        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }    
    
    return Results.NotFound();
});

app.Run();
