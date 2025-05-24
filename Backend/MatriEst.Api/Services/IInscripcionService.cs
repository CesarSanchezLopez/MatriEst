namespace MatriEst.Api.Services
{
    public interface IInscripcionService
    {
        Task<(bool Success, string Message)> InscribirEstudianteAsync(int estudianteId, List<int> materiaIds);
    }
}
