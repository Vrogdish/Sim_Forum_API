using Sim_Forum.DTOs.Forum.Categories;

namespace Sim_Forum.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<IEnumerable<CategoryWithThreadsDto>> GetWithThreadsAsync(int limit = 5);
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto?> GetBySlugAsync(string slug);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> UpdateAsync(int id, CreateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
