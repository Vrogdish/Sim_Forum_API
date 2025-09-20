using Sim_Forum.DTOs.Forum.Threads;

namespace Sim_Forum.Services.Interfaces
{
    public interface IThreadService
    {
        Task<IEnumerable<ThreadDto>> GetByCategoryIdAsync(int categoryId);
        Task<ThreadDto?> GetByIdAsync(int id);
        Task<ThreadDto> CreateAsync(CreateThreadDto dto, int userId);
        Task<bool> UpdateAsync(int id, UpdateThreadDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
