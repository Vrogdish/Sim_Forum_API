using Sim_Forum.DTOs.Forum.Threads;

namespace Sim_Forum.DTOs.Forum.Categories
{
    public class CategoryWithThreadsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TotalThreads { get; set; }
        public List<ThreadDto> Threads { get; set; } = new();
    }
}
