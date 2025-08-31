namespace Sim_Forum.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<Thread> Threads { get; set; } = null!;
    }
}
