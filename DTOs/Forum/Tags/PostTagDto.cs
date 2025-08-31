namespace Sim_Forum.DTOs.Forum.Tags
{
    public class PostTagDto
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;
    }
}
