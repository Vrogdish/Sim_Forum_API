using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Users;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;


namespace Sim_Forum.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ForumContext _context;
        private readonly IHostEnvironment _env;
        private readonly IEmailService _emailService;

        public UserService(ForumContext context, IHostEnvironment env, IEmailService emailService)
        {
            _context = context;
            _env = env;
            _emailService = emailService;
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

        public async Task<UserDto?> UploadAvatarAsync(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            // Dossier des avatars
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads", "avatars");
            Directory.CreateDirectory(uploadsFolder);

            // Nom du fichier
            var extension = Path.GetExtension(file.FileName).ToLower();
            if ( extension != ".png")
                throw new Exception("Seules les images PNG sont autorisées.");

            var fileName = $"{userId}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            // Met à jour le chemin relatif en DB
            var relativePath = $"/uploads/avatars/{fileName}";
            user.AvatarUrl = relativePath;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Retourne le DTO mis à jour
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                AvatarUrl = relativePath,
                Signature = user.Signature,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            // Vérifie l'ancien mot de passe
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.PasswordHash))
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdminResetPasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SendPasswordResetTokenAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) throw new InvalidOperationException("Utilisateur introuvable");

            var token = Guid.NewGuid().ToString("N"); // token unique
            var expiresAt = DateTime.UtcNow.AddHours(1);

            _context.PasswordResets.Add(new PasswordReset
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = expiresAt
            });
            await _context.SaveChangesAsync();

            var resetLink = $"https://simforum.com/reset-password?token={token}";

            // Envoi email (via ton service SMTP ou SendGrid)
            await _emailService.SendAsync(user.Email,
                   "Réinitialisation de votre mot de passe",
                    $"Cliquez sur ce lien pour réinitialiser votre mot de passe : {resetLink}");

            return true;
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var reset = await _context.PasswordResets
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token && r.ExpiresAt > DateTime.UtcNow);

            if (reset == null) return false;

            reset.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

            _context.PasswordResets.Remove(reset); // consommer le token
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
