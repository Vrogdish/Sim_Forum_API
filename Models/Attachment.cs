using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sim_Forum.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; } = null!;

        [MaxLength(255)]
        public string FileUrl { get; set; } = null!;

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}
