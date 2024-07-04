using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

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

app.Run();
