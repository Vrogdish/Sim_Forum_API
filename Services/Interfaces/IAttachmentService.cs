using Sim_Forum.DTOs.Forum.Attachments;

namespace Sim_Forum.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentDto>> GetAllByPostAsync(int postId);
        Task<AttachmentDto?> GetByIdAsync(int id);
        Task<AttachmentDto> CreateAsync(CreateAttachmentDto dto);
        Task<bool> UpdateAsync(int id, UpdateAttachmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
