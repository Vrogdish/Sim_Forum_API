using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.DTOs.Forum.Categories;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Controllers
{
    [ApiController]
    [Route("api/category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("with-threads")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CategoryWithThreadsDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryWithThreadsDto>>> GetCategoriesWithThreads([FromQuery] int limit = 5)
        {
            var categories = await _categoryService.GetWithThreadsAsync(limit);
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound(new ErrorResponseDto { message = "Category not found" });
            return Ok(category);
        }

        [HttpGet("{slug}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategoryBySlug(string slug)
        {
            var category = await _categoryService.GetBySlugAsync(slug);
            if (category == null)
                return NotFound(new ErrorResponseDto { message = "Category not found" });
            return Ok(category);
        }



        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
        {
            var category = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto dto)
        {
            var success = await _categoryService.UpdateAsync(id, dto);
            if (!success)
                return NotFound(new ErrorResponseDto { message = "Category not found" });

            var updatedCategory = await _categoryService.GetByIdAsync(id);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteAsync(id);
            if (!success)
                return NotFound(new ErrorResponseDto { message = "Category not found" });

            return Ok(new { success = true, message = "Category deleted successfully." });
        }

    }
}
