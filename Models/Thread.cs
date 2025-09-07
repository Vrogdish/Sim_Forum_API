using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Sim_Forum.Models
{
    public class Thread
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = null!;
    }
}
