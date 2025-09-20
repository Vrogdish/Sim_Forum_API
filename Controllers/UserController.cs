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
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
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
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var success = await _userService.DeleteAsync(userId);
            if (!success) return NotFound();

            return Ok(new { success = true, message = "User deleted successfully." });
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
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

        // POST: api/User/avatar
        [HttpPost("me/avatar")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!); 

            var userDto = await _userService.UploadAvatarAsync(userId, file);
            if (userDto == null)
                return NotFound("Utilisateur introuvable ou fichier invalide.");

            return Ok(userDto);
        }

        // POST: api/user/{id}/avatar
        [HttpPost("{id}/avatar")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadAvatarByAdmin(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Aucun fichier envoyé.");

            var userDto = await _userService.UploadAvatarAsync(id, file);
            if (userDto == null)
                return NotFound("Utilisateur introuvable ou fichier invalide.");

            return Ok(userDto);
        }

        [HttpPost("me/change-password")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var success = await _userService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            if (!success)
            {
                return BadRequest(new ErrorResponseDto
                {
                    statusCode = 400,
                    message = "Mot de passe actuel incorrect."
                });
            }

            return Ok(new { success = true, message = "Mot de passe changé avec succés" });
        }

        [Authorize (Roles = "admin")]
        [HttpPost("{id}/change-password")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(int id, ChangePasswordDto dto)
        {
            var success = await _userService.AdminResetPasswordAsync(id, dto.NewPassword);
            if (!success)
            {
                return BadRequest(new ErrorResponseDto
                {
                    statusCode = 400,
                    message = "Erreur lors du changement de mot de passe."
                });
            }
            return Ok(new { success = true, message = "Mot de passe changé avec succés" });

        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var success = await _userService.SendPasswordResetTokenAsync(dto.Email);
            return Ok(new { success = true });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var success = await _userService.ResetPasswordAsync(dto.Token, dto.NewPassword);
            if (!success)
            {
                return BadRequest(new { success = false, message = "Invalid or expired token." });
            }

            return Ok(new { success = true, message = "Password reset successfully." });
        }
    }
}
