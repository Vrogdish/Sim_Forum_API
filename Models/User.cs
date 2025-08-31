using Microsoft.Extensions.Hosting;

namespace Sim_Forum.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "user";
        public string AvatarUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Thread> Threads { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = null!;
        public ICollection<PostLike> PostLikes { get; set; } = null!;

    }
}
