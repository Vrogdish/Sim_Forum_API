using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.DTOs.Forum.Attachments;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/attachment")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet("post/{postId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<AttachmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<AttachmentDto>>> GetAttachmentsByPost(int postId)
        {
            var attachments = await _attachmentService.GetAllByPostAsync(postId);
            return Ok(attachments);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AttachmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AttachmentDto>> GetAttachment(int id)
        {
            var attachment = await _attachmentService.GetByIdAsync(id);
            if (attachment == null) return NotFound();
            return Ok(attachment);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AttachmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AttachmentDto>> CreateAttachment(CreateAttachmentDto dto)
        {
            var attachment = await _attachmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAttachment), new { id = attachment.Id }, attachment);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AttachmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAttachment(int id, UpdateAttachmentDto dto)
        {
            var success = await _attachmentService.UpdateAsync(id, dto);
            if (!success) return NotFound();

            var updatedAttachment = await _attachmentService.GetByIdAsync(id);
            return Ok(updatedAttachment);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var success = await _attachmentService.DeleteAsync(id);
            if (!success) return NotFound(new ErrorResponseDto { message = "Attachment not found." });

            return Ok(new { success = true, message = "Attachment deleted successfully." });
        }
    }
}
