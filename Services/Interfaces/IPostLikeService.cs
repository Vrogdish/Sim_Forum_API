namespace Sim_Forum.Services.Interfaces
{
    public interface IPostLikeService
    {
        Task<bool> LikeAsync(int postId, int userId);
        Task<bool> UnlikeAsync(int postId, int userId);
        Task<int> GetLikesCountAsync(int postId);
        Task<bool> HasUserLikedAsync(int postId, int userId);
    }
}
