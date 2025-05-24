using MatriEst.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatriEst.Api.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<EstudianteMateria> EstudianteMaterias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de clave compuesta
            modelBuilder.Entity<EstudianteMateria>()
                .HasKey(em => new { em.EstudianteId, em.MateriaId });

            // Relaciones muchos-a-muchos explícitas
            modelBuilder.Entity<EstudianteMateria>()
                .HasOne(em => em.Estudiante)
                .WithMany(e => e.Materias)
                .HasForeignKey(em => em.EstudianteId);

            modelBuilder.Entity<EstudianteMateria>()
                .HasOne(em => em.Materia)
                .WithMany(m => m.Estudiantes)
                .HasForeignKey(em => em.MateriaId);
        }
    }
}
