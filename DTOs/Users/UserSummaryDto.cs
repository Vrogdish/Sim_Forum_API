namespace Sim_Forum.DTOs.Users
{
    public class UserSummaryDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? AvatarUrl { get; set; }
    }
}
