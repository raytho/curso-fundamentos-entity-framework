using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tarea>(tarea => 
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId);
            
            tarea.HasOne(t => t.Categoria).WithMany(t => t.Tareas).HasForeignKey(t => t.CategoriaId);

            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(150);
            tarea.Property(t => t.Descripcion);
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);
            tarea.Ignore(t => t.Resumen);
        });

        builder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(c => c.CategoriaId);
            categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.Descripcion);
            categoria.Property(c => c.Peso);
        });
    }
}