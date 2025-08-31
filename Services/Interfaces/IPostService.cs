using Sim_Forum.DTOs.Forum.Threads;

namespace Sim_Forum.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetByThreadAsync(int threadId);
        Task<PostDto?> GetByIdAsync(int id);
        Task<PostDto> CreateAsync(CreatePostDto dto, int userId);
        Task<bool> UpdateAsync(int id, UpdatePostDto dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
