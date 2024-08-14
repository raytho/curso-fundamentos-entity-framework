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
        List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea{ TareaId = Guid.Parse("efcca2b4-ad72-46fa-821d-f46997fdbdc5"), CategoriaId = Guid.Parse("9fb7df3f-041c-4b46-8d5b-4e4af127c870"), PrioridadTarea = Prioridad.Media, Titulo = "Pago servicios publicos", FechaCreacion = DateTime.Now});
        tareasInit.Add(new Tarea { TareaId = Guid.Parse("068df6fd-5b6e-4622-b85e-a657db87e15b"), CategoriaId = Guid.Parse("42d74f6a-924d-4ba6-bc72-2f7c31f71e4c"), PrioridadTarea = Prioridad.Baja, Titulo = "Ver serie shogun", FechaCreacion = DateTime.Now });
        
        builder.Entity<Tarea>(tarea => 
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId);
            
            tarea.HasOne(t => t.Categoria).WithMany(t => t.Tareas).HasForeignKey(t => t.CategoriaId);

            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(150);
            tarea.Property(t => t.Descripcion).IsRequired(false);
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);
            tarea.Ignore(t => t.Resumen);

            tarea.HasData(tareasInit);
        });

        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria{ CategoriaId = Guid.Parse("42d74f6a-924d-4ba6-bc72-2f7c31f71e4c"), Nombre = "Actividades pendientes", Peso = 20 });
        categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("9fb7df3f-041c-4b46-8d5b-4e4af127c870"), Nombre = "Actividades personales", Peso = 50 });
        categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("34dfb625-4364-4454-ace0-c5459e33da22"), Nombre = "Actividades deportivas", Peso = 25 });
        categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("439eac85-642a-499a-aeae-5624672135d7"), Nombre = "Actividades recreativas", Peso = 15 });

        builder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(c => c.CategoriaId);
            categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.Descripcion).IsRequired(false);
            categoria.Property(c => c.Peso);

            categoria.HasData(categoriasInit);
        });
    }
}