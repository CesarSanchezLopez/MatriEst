using MatriEst.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatriEst.Api.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Aplicar migraciones si faltan
            await context.Database.MigrateAsync();

            if (!context.Profesores.Any())
            {
                var profesores = new List<Profesor>
                {
                    new() { Nombre = "Prof. Ana García" },
                    new() { Nombre = "Prof. Juan López" },
                    new() { Nombre = "Prof. Marta Ruiz" },
                    new() { Nombre = "Prof. Carlos Pérez" },
                    new() { Nombre = "Prof. Laura Díaz" }
                };

                await context.Profesores.AddRangeAsync(profesores);
                await context.SaveChangesAsync();
            }

            if (!context.Materias.Any())
            {
                var profesores = await context.Profesores.ToListAsync();

                var materias = new List<Materia>();

                for (int i = 0; i < profesores.Count; i++)
                {
                    materias.Add(new Materia { Nombre = $"Materia {(i * 2) + 1}", ProfesorId = profesores[i].Id });
                    materias.Add(new Materia { Nombre = $"Materia {(i * 2) + 2}", ProfesorId = profesores[i].Id });
                }

                await context.Materias.AddRangeAsync(materias);
                await context.SaveChangesAsync();
            }

            if (!context.Estudiantes.Any())
            {
                var estudiantes = new List<Estudiante>
                {
                    new() { Nombre = "Luis Fernández" },
                    new() { Nombre = "Carla Romero" },
                    new() { Nombre = "David Torres" }
                };

                await context.Estudiantes.AddRangeAsync(estudiantes);
                await context.SaveChangesAsync();
            }
        }
    }
}
