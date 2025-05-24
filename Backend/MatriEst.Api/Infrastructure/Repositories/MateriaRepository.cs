using MatriEst.Api.Core.Entities;
using MatriEst.Api.Core.Interfaces;
using MatriEst.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MatriEst.Api.Infrastructure.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly ApplicationDbContext _context;

        public MateriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            return await _context.Materias
                .Include(m => m.Profesor)
                .Include(m => m.Estudiantes)
                .ThenInclude(em => em.Estudiante)
                .ToListAsync();
        }

        public async Task<Materia?> GetByIdAsync(int id)
        {
            return await _context.Materias
                .Include(m => m.Profesor)
                .Include(m => m.Estudiantes)
                .ThenInclude(em => em.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
