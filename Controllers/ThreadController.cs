using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.DTOs.Forum.Threads;
using Sim_Forum.Services.Interfaces;
using System.Security.Claims;

namespace Sim_Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/thread")]
    public class ThreadController : ControllerBase
    {
        private readonly IThreadService _threadService;

        public ThreadController(IThreadService threadService)
        {
            _threadService = threadService;
        }

        // GET: api/Thread/category/5
        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<ThreadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ThreadDto>>> GetThreadsByCategory(int categoryId)
        {
            var threads = await _threadService.GetByCategoryIdAsync(categoryId);
            return Ok(threads);
        }

        // GET: api/Thread/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ThreadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ThreadDto>> GetThread(int id)
        {
            var thread = await _threadService.GetByIdAsync(id);
            if (thread == null) return NotFound();
            return Ok(thread);
        }

        // POST: api/Thread
        [HttpPost]
        [ProducesResponseType(typeof(ThreadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ThreadDto>> CreateThread(CreateThreadDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var thread = await _threadService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetThread), new { id = thread.Id }, thread);
        }

        // PUT: api/Thread/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ThreadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateThread(int id, UpdateThreadDto dto)
        {
            if (!await CanAccessThread(id)) return Forbid();

            var success = await _threadService.UpdateAsync(id, dto);
            if (!success) return NotFound();

            var updatedThread = await _threadService.GetByIdAsync(id);
            return Ok(updatedThread);
        }

        // DELETE: api/Thread/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteThread(int id)
        {

            var success = await _threadService.DeleteAsync(id);
            if (!success) return NotFound();

            return Ok(new { success = true, message = "Thread deleted successfully." });
        }

        // Vérifie si l'utilisateur peut modifier/supprimer un thread (propriétaire ou admin)
        private async Task<bool> CanAccessThread(int threadId)
        {
            var thread = await _threadService.GetByIdAsync(threadId);
            if (thread == null) return false;

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isAdmin = User.IsInRole("admin");

            return isAdmin || thread.UserId == userId;
        }
    }

}
