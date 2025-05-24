using MatriEst.Api.Core.Entities;

namespace MatriEst.Api.Core.Interfaces
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<Materia?> GetByIdAsync(int id);
    }
}
