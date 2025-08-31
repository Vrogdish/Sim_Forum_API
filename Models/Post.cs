using System.Net.Mail;

namespace Sim_Forum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int ThreadId { get; set; }
        public Thread Thread { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<PostLike> PostLikes { get; set; } = null!;
        public ICollection<Attachment> Attachments { get; set; } = null!;
        public ICollection<PostTag> PostTags { get; set; } = null!;
    }
}
