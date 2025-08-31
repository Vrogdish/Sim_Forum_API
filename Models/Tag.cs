﻿namespace Sim_Forum.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<PostTag> PostTags { get; set; } = null!;
    }
}
