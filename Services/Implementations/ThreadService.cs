using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Forum.Threads;
using Sim_Forum.Services.Interfaces;
using Thread = Sim_Forum.Models.Thread;


namespace Sim_Forum.Services.Implementations
{
    public class ThreadService : IThreadService
    {
        private readonly ForumContext _context;

        public ThreadService(ForumContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ThreadDto>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Threads
                .Where(t => t.CategoryId == categoryId)
                .Include(t => t.Category)
                .Include(t => t.User)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new ThreadDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category != null ? t.Category.Name : string.Empty,
                    UserId = t.UserId,
                    Username = t.User != null ? t.User.Username : string.Empty,
                    TotalPosts = t.Posts.Count,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ThreadDto?> GetByIdAsync(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            if (thread == null) return null;

            return new ThreadDto
            {
                Id = thread.Id,
                Title = thread.Title,
                CategoryId = thread.CategoryId,
                CategoryName = thread.Category != null ? thread.Category.Name : string.Empty,
                UserId = thread.UserId,
                Username = thread.User != null ? thread.User.Username : string.Empty,
                TotalPosts = thread.Posts.Count,
                CreatedAt = thread.CreatedAt
            };
        }

        public async Task<ThreadDto> CreateAsync(CreateThreadDto dto, int userId)
        {
            var thread = new Thread
            {
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Threads.Add(thread);
            await _context.SaveChangesAsync();

            return new ThreadDto
            {
                Id = thread.Id,
                Title = thread.Title,
                CategoryId = thread.CategoryId,
                CategoryName = thread.Category != null ? thread.Category.Name : string.Empty,
                UserId = thread.UserId,
                Username = thread.User != null ? thread.User.Username : string.Empty,
                TotalPosts = 1,
                CreatedAt = thread.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateThreadDto dto)
        {
            var thread = await _context.Threads.FindAsync(id);
            if (thread == null) return false;

            if (!string.IsNullOrEmpty(dto.Title)) thread.Title = dto.Title;
            if (dto.CategoryId.HasValue) thread.CategoryId = dto.CategoryId.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            if (thread == null) return false;

            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
