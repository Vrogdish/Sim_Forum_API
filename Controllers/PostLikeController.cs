using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.Services.Interfaces;
using System.Security.Claims;

namespace Sim_Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/postlike")]
    public class PostLikeController : ControllerBase
    {
        private readonly IPostLikeService _likeService;

        public PostLikeController(IPostLikeService likeService)
        {
            _likeService = likeService;
        }

        // POST: api/PostLike/5
        [HttpPost("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var success = await _likeService.LikeAsync(postId, userId);
            if (!success) return BadRequest(new ErrorResponseDto { message = "Post already liked" });
            return Ok();
        }

        // DELETE: api/PostLike/5
        [HttpDelete("{postId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UnlikePost(int postId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var success = await _likeService.UnlikeAsync(postId, userId);
            if (!success) return NotFound(new ErrorResponseDto { message = "Like not found" });
            return NoContent();
        }

        // GET: api/PostLike/5/count
        [HttpGet("{postId}/count")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLikesCount(int postId)
        {
            var count = await _likeService.GetLikesCountAsync(postId);
            return Ok(count);
        }

        // GET: api/PostLike/5/hasLiked
        [HttpGet("{postId}/hasLiked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> HasLiked(int postId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var hasLiked = await _likeService.HasUserLikedAsync(postId, userId);
            return Ok(hasLiked);
        }
    }
}
