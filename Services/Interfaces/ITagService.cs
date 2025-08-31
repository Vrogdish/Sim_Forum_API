using Sim_Forum.DTOs.Forum.Tags;

namespace Sim_Forum.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<TagDto?> GetByIdAsync(int id);
        Task<TagDto> CreateAsync(CreateTagDto dto);
        Task<bool> UpdateAsync(int id, UpdateTagDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
