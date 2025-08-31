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

        public async Task<IEnumerable<ThreadDto>> GetAllAsync()
        {
            return await _context.Threads
                .Select(t => new ThreadDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    CategoryId = t.CategoryId,
                    UserId = t.UserId,
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
                UserId = thread.UserId,
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
                UserId = thread.UserId,
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
