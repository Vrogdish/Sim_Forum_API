using Sim_Forum.DTOs.Forum.Tags;

namespace Sim_Forum.Services.Interfaces
{
    public interface IPostTagService
    {
        Task<IEnumerable<PostTagDto>> GetAllByPostAsync(int postId);
        Task<PostTagDto?> GetByPostAndTagAsync(int postId, int tagId);
        Task<PostTagDto> CreateAsync(CreatePostTagDto dto);
        Task<bool> DeleteAsync(int postId, int tagId);
    }
}
