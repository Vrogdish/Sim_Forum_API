using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;


namespace Sim_Forum.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(255)]
        public string PasswordHash { get; set; } = null!;

        [MaxLength(20)]
        public string Role { get; set; } = "user";

        [MaxLength(255)]
        public string? AvatarUrl { get; set; }

        public string? Signature { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Thread> Threads { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = null!;
        public ICollection<PostLike> PostLikes { get; set; } = null!;

    }
}
