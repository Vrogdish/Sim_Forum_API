using Sim_Forum.DTOs.Users;

namespace Sim_Forum.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<UserDto?> UploadAvatarAsync(int id, IFormFile file);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<bool> AdminResetPasswordAsync(int userId, string newPassword);
        Task<bool> SendPasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);


    }
}
