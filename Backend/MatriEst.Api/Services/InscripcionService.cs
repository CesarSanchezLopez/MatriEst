using MatriEst.Api.Core.Entities;
using MatriEst.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MatriEst.Api.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly ApplicationDbContext _context;

        public InscripcionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message)> InscribirEstudianteAsync(int estudianteId, List<int> materiaIds)
        {
            if (materiaIds.Count > 3)
                return (false, "Solo puedes inscribirte en hasta 3 materias.");

            var estudiante = await _context.Estudiantes
                .Include(e => e.Materias)
                .ThenInclude(em => em.Materia)
                .ThenInclude(m => m.Profesor)
                .FirstOrDefaultAsync(e => e.Id == estudianteId);

            if (estudiante == null)
                return (false, "Estudiante no encontrado.");

            var materias = await _context.Materias
                .Include(m => m.Profesor)
                .Where(m => materiaIds.Contains(m.Id))
                .ToListAsync();

            var profesores = materias.Select(m => m.ProfesorId).ToList();
            if (profesores.Distinct().Count() != profesores.Count)
                return (false, "No puedes inscribirte en materias del mismo profesor.");

            estudiante.Materias.Clear();
            foreach (var materiaId in materiaIds)
            {
                estudiante.Materias.Add(new EstudianteMateria
                {
                    EstudianteId = estudianteId,
                    MateriaId = materiaId
                });
            }

            await _context.SaveChangesAsync();
            return (true, "Inscripción realizada correctamente.");
        }
    }
}
