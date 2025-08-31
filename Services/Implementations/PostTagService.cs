using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Forum.Tags;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{
    public class PostTagService : IPostTagService
    {
        private readonly ForumContext _context;

        public PostTagService(ForumContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostTagDto>> GetAllByPostAsync(int postId)
        {
            return await _context.PostTags
                .Where(pt => pt.PostId == postId)
                .Include(pt => pt.Tag)
                .Select(pt => new PostTagDto
                {
                    PostId = pt.PostId,
                    TagId = pt.TagId,
                    TagName = pt.Tag.Name
                })
                .ToListAsync();
        }

        public async Task<PostTagDto?> GetByPostAndTagAsync(int postId, int tagId)
        {
            var pt = await _context.PostTags
                .Include(x => x.Tag)
                .FirstOrDefaultAsync(x => x.PostId == postId && x.TagId == tagId);

            if (pt == null) return null;

            return new PostTagDto
            {
                PostId = pt.PostId,
                TagId = pt.TagId,
                TagName = pt.Tag.Name
            };
        }

        public async Task<PostTagDto> CreateAsync(CreatePostTagDto dto)
        {
            var exists = await _context.PostTags.AnyAsync(pt => pt.PostId == dto.PostId && pt.TagId == dto.TagId);
            if (exists) throw new InvalidOperationException("This tag is already associated with the post.");

            var postTag = new PostTag
            {
                PostId = dto.PostId,
                TagId = dto.TagId
            };

            _context.PostTags.Add(postTag);
            await _context.SaveChangesAsync();

            var tag = await _context.Tags.FindAsync(dto.TagId);

            return new PostTagDto
            {
                PostId = dto.PostId,
                TagId = dto.TagId,
                TagName = tag!.Name
            };
        }

        public async Task<bool> DeleteAsync(int postId, int tagId)
        {
            var postTag = await _context.PostTags.FindAsync(postId, tagId);
            if (postTag == null) return false;

            _context.PostTags.Remove(postTag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
