using Sim_Forum.DTOs.Users;

namespace Sim_Forum.DTOs.Forum.Threads
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ThreadId { get; set; }
        public UserSummaryDto User { get; set; } = null!;
        public List<int>? TagIds { get; set; }


    }
}
