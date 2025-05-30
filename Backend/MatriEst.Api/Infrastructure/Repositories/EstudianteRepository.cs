﻿using MatriEst.Api.Core.Entities;
using MatriEst.Api.Core.Interfaces;
using MatriEst.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MatriEst.Api.Infrastructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly ApplicationDbContext _context;

        public EstudianteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes
                .Include(e => e.Materias)
                .ThenInclude(em => em.Materia)
                .ThenInclude(m => m.Profesor)
        .ToListAsync();
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _context.Estudiantes
                .Include(e => e.Materias)
                .ThenInclude(em => em.Materia)
                .ThenInclude(m => m.Profesor)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarInscripcion(int estudianteId, int materiaId)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Materias)
                .FirstOrDefaultAsync(e => e.Id == estudianteId);

            //if (estudiante == null)
            //    return false;

            var inscripcion = estudiante.Materias.FirstOrDefault(em => em.MateriaId == materiaId);
            //if (inscripcion == null)
            //    return false;

            estudiante.Materias.Remove(inscripcion);
            await _context.SaveChangesAsync();

           // return true;
        }

    }
}
