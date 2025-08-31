using Sim_Forum.Controllers;
using Sim_Forum.DTOs.Auth;
using Sim_Forum.Models;

namespace Sim_Forum.Services.Interfaces
{

        public interface IAuthService
        {
            Task<User?> RegisterAsync(RegisterDto dto);
            Task<string?> LoginAsync(LoginDto dto);
        }

}
