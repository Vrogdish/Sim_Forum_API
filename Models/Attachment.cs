using System.ComponentModel.DataAnnotations.Schema;

namespace Sim_Forum.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        public string FileName { get; set; } = null!;

        public string FileUrl { get; set; } = null!;

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}
