namespace Sim_Forum.DTOs.Forum.Threads
{
    public class CreateThreadDto
    {
        public string Title { get; set; } = null!;
        public int CategoryId { get; set; }
    }


}
