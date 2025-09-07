using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.DTOs.Forum.Tags;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posttag")]
    public class PostTagController : ControllerBase
    {
        private readonly IPostTagService _postTagService;

        public PostTagController(IPostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        // GET: api/PostTag/post/5
        [HttpGet("post/{postId}")]
        [ProducesResponseType(typeof(IEnumerable<PostTagDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PostTagDto>>> GetTagsByPost(int postId)
        {
            var tags = await _postTagService.GetAllByPostAsync(postId);
            return Ok(tags);
        }

        // POST: api/PostTag
        [HttpPost]
        [ProducesResponseType(typeof(PostTagDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PostTagDto>> CreatePostTag(CreatePostTagDto dto)
        {
            try
            {
                var postTag = await _postTagService.CreateAsync(dto);
                return Ok(postTag); // Retourne directement l'objet créé
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseDto { message = ex.Message });
            }
        }

        // DELETE: api/PostTag/post/5/tag/2
        [HttpDelete("post/{postId}/tag/{tagId}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePostTag(int postId, int tagId)
        {
            var success = await _postTagService.DeleteAsync(postId, tagId);
            if (!success) return NotFound(new ErrorResponseDto { message = "PostTag not found." });

            return Ok(new { success = true, message = "PostTag deleted successfully." });
        }
    }
}
