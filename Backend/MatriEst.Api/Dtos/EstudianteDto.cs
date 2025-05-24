namespace MatriEst.Api.Dtos
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<MateriaDto> Materias { get; set; } = new();
    }
}
