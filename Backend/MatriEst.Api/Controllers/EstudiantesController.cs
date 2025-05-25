using MatriEst.Api.Core.Entities;
using MatriEst.Api.Core.Interfaces;
using MatriEst.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatriEst.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteRepository _estudianteRepository;

        public EstudiantesController(IEstudianteRepository estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _estudianteRepository.GetAllAsync();

            var result = estudiantes.Select(e => new
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Materias = e.Materias.Select(em => new
                {
                    Nombre = em.Materia.Nombre,
                    Profesor = em.Materia.Profesor?.Nombre ?? "Sin asignar"
                })
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estudiante = await _estudianteRepository.GetByIdAsync(id);
            if (estudiante == null)
                return NotFound();

            var dto = new EstudianteDto
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Materias = estudiante.Materias.Select(em => new MateriaDto
                {
                    Id = em.Materia.Id,
                    Nombre = em.Materia.Nombre,
                    ProfesorNombre = em.Materia.Profesor?.Nombre ?? "Sin asignar"
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Estudiante estudiante)
        {
            await _estudianteRepository.AddAsync(estudiante);
            return CreatedAtAction(nameof(GetById), new { id = estudiante.Id }, estudiante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
                return BadRequest();

            await _estudianteRepository.UpdateAsync(estudiante);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _estudianteRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
