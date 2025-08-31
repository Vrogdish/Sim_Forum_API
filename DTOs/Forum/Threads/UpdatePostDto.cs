namespace Sim_Forum.DTOs.Forum.Threads
{
    public class UpdatePostDto
    {
        public string? Content { get; set; }
        public List<int>? TagIds { get; set; }  // Mise à jour des tags


    }
}
