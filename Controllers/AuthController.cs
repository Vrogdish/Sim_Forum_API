using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim_Forum.DTOs.Auth;
using Sim_Forum.DTOs.Errors;
using Sim_Forum.DTOs.Users;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Enregistre un nouvel utilisateur.
        /// </summary>
        /// <param name="dto">Données d'inscription (email, username, mot de passe).</param>
        /// <response code="200">Utilisateur créé avec succès.</response>
        /// <response code="400">Email déjà utilisé.</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto);

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return Ok(userDto);
        }

        /// <summary>
        /// Authentifie un utilisateur et retourne un token JWT.
        /// </summary>
        /// <param name="dto">Identifiants de connexion (email, mot de passe).</param>
        /// <response code="200">Connexion réussie, retourne un token JWT.</response>
        /// <response code="401">Identifiants invalides.</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
            {
                return Unauthorized(new ErrorResponseDto
                {
                    statusCode = 401,
                    message = "Invalid credentials."
                });
            }

            return Ok(new TokenResponseDto { Token = token });
        }
    }
}
