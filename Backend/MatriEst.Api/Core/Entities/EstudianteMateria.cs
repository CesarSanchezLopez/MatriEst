namespace MatriEst.Api.Core.Entities
{
    public class EstudianteMateria
    {
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; } = null!;

        public int MateriaId { get; set; }
        public Materia Materia { get; set; } = null!;
    }
}
