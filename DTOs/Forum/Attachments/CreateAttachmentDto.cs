namespace Sim_Forum.DTOs.Forum.Attachments
{
    public class CreateAttachmentDto
    {
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public int PostId { get; set; }
    }
}
