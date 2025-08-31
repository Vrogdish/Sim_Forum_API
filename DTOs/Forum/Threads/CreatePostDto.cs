namespace Sim_Forum.DTOs.Forum.Threads
{
    public class CreatePostDto
    {
        public string Content { get; set; } = null!;
        public int ThreadId { get; set; }
        public List<int>? TagIds { get; set; }  
    }
}
