using MatriEst.Api.Core.Interfaces;
using MatriEst.Api.Dtos;
using MatriEst.Api.Infrastructure.Data;
using MatriEst.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MatriEst.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaRepository _materiaRepository;
        private readonly IInscripcionService _inscripcionService;

        public MateriasController(
            IMateriaRepository materiaRepository,
            IInscripcionService inscripcionService)
        {
            _materiaRepository = materiaRepository;
            _inscripcionService = inscripcionService;
        }

        // GET: api/materias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materias = await _materiaRepository.GetAllAsync();

            var result = materias.Select(m => new MateriaDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                ProfesorNombre = m.Profesor?.Nombre ?? "Sin asignar"
            }).ToList();

            return Ok(result);
        }

        // GET: api/materias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var materia = await _materiaRepository.GetByIdAsync(id);
            if (materia == null)
                return NotFound("Materia no encontrada.");

            return Ok(materia);
        }

        // POST: api/materias/inscribir?estudianteId=1
        [HttpPost("inscribir")]
        public async Task<IActionResult> Inscribir(int estudianteId, [FromBody] List<int> materiaIds)
        {
            var (success, message) = await _inscripcionService.InscribirEstudianteAsync(estudianteId, materiaIds);

            if (!success)
                return BadRequest(new { message });

            return Ok(new { message });
        }

        // GET: api/materias/5/compañeros
        [HttpGet("{materiaId}/compañeros")]
        public async Task<IActionResult> GetCompañeros(int materiaId)
        {
            var materia = await _materiaRepository.GetByIdAsync(materiaId);

            if (materia == null)
                return NotFound("Materia no encontrada.");

            var compañeros = materia.Estudiantes
                .Select(em => new
                {
                    em.Estudiante.Id,
                    em.Estudiante.Nombre
                })
                .ToList();

            return Ok(compañeros);
        }
    }

}
