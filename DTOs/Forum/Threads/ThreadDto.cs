namespace Sim_Forum.DTOs.Forum.Threads
{
    public class ThreadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
