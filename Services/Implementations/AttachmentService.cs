using Sim_Forum.Data;
using Sim_Forum.DTOs.Forum.Attachments;
using Microsoft.EntityFrameworkCore;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{

        public class AttachmentService : IAttachmentService
        {
            private readonly ForumContext _context;

            public AttachmentService(ForumContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<AttachmentDto>> GetAllByPostAsync(int postId)
            {
                return await _context.Attachments
                    .Where(a => a.PostId == postId)
                    .Select(a => new AttachmentDto
                    {
                        Id = a.Id,
                        FileName = a.FileName,
                        FileUrl = a.FileUrl,
                        PostId = a.PostId
                    })
                    .ToListAsync();
            }

            public async Task<AttachmentDto?> GetByIdAsync(int id)
            {
                var attachment = await _context.Attachments.FindAsync(id);
                if (attachment == null) return null;

                return new AttachmentDto
                {
                    Id = attachment.Id,
                    FileName = attachment.FileName,
                    FileUrl = attachment.FileUrl,
                    PostId = attachment.PostId
                };
            }

            public async Task<AttachmentDto> CreateAsync(CreateAttachmentDto dto)
            {
                var attachment = new Attachment
                {
                    FileName = dto.FileName,
                    FileUrl = dto.FileUrl,
                    PostId = dto.PostId
                };

                _context.Attachments.Add(attachment);
                await _context.SaveChangesAsync();

                return new AttachmentDto
                {
                    Id = attachment.Id,
                    FileName = attachment.FileName,
                    FileUrl = attachment.FileUrl,
                    PostId = attachment.PostId
                };
            }

            public async Task<bool> UpdateAsync(int id, UpdateAttachmentDto dto)
            {
                var attachment = await _context.Attachments.FindAsync(id);
                if (attachment == null) return false;

                if (!string.IsNullOrEmpty(dto.FileName)) attachment.FileName = dto.FileName;
                if (!string.IsNullOrEmpty(dto.FileUrl)) attachment.FileUrl = dto.FileUrl;

                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var attachment = await _context.Attachments.FindAsync(id);
                if (attachment == null) return false;

                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    
}
