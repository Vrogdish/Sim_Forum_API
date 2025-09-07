using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.DTOs.Users;
using Sim_Forum.Services.Interfaces;
using System.Security.Claims;

namespace Sim_Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/5

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        // PUT: api/User/5
        [HttpPut("me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var success = await _userService.UpdateAsync(userId, dto);
            if (!success) return NotFound();

            var updatedUser = await _userService.GetByIdAsync(userId);

            return Ok(updatedUser);
        }

        // DELETE: api/User/5
        [HttpDelete("me")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var success = await _userService.DeleteAsync(userId);
            if (!success) return NotFound();

            return Ok(new { success = true, message = "User deleted successfully." });
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userService.GetByIdAsync(userId);
            if (user == null) return NotFound();

            return Ok(user);
        }

        // DELETE pour un admin
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserByAdmin(int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success) return NotFound();

            return Ok(new { success = true, message = "User deleted successfully." });
        }


    }
}
