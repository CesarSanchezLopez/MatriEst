namespace MatriEst.Api.Core.Entities
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<EstudianteMateria> Materias { get; set; } = new List<EstudianteMateria>();
    }
}
