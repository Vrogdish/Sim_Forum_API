using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Users;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ForumContext _context;

        public UserService(ForumContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,
                    AvatarUrl = u.AvatarUrl,
                    Signature = u.Signature,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                AvatarUrl = user.AvatarUrl,
                Signature = user.Signature,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            if (!string.IsNullOrEmpty(dto.AvatarUrl))
                user.AvatarUrl = dto.AvatarUrl;

            if (!string.IsNullOrEmpty(dto.Signature))
                user.Signature = dto.Signature;

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
