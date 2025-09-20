using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Forum.Categories;
using Sim_Forum.DTOs.Forum.Threads;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ForumContext _context;

        public CategoryService(ForumContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<CategoryWithThreadsDto>> GetWithThreadsAsync(int limit = 5)
        {
            return await _context.Categories
                .Select(c => new CategoryWithThreadsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    TotalThreads = c.Threads.Count, 
                    Threads = c.Threads
                        .OrderByDescending(t => t.CreatedAt)
                        .Take(limit)
                        .Select(t => new ThreadDto
                        {
                            Id = t.Id,
                            Title = t.Title,
                            UserId = t.UserId,
                            Username = t.User.Username,
                            CreatedAt = t.CreatedAt
                        })
                        .ToList()
                })
                .ToListAsync();
        }


        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto?> GetByNameAsync(string name)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
            if (category == null) return null;
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.Name = dto.Name;
            category.Description = dto.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
