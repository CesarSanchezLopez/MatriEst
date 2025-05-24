namespace MatriEst.Api.Core.Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; } = 3;

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; } = null!;

        public ICollection<EstudianteMateria> Estudiantes { get; set; } = new List<EstudianteMateria>();
    }
}
