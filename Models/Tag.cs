using System.ComponentModel.DataAnnotations;

namespace Sim_Forum.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<PostTag> PostTags { get; set; } = null!;
    }
}
