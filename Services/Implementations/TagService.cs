using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Forum.Tags;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ForumContext _context;

        public TagService(ForumContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            return await _context.Tags
                .Select(t => new TagDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<TagDto?> GetByIdAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return null;

            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task<TagDto> CreateAsync(CreateTagDto dto)
        {
            var tag = new Tag { Name = dto.Name };
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTagDto dto)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            tag.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null) return false;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
