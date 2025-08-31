using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{
    public class PostLikeService : IPostLikeService
    {
        private readonly ForumContext _context;

        public PostLikeService(ForumContext context)
        {
            _context = context;
        }

        public async Task<bool> LikeAsync(int postId, int userId)
        {
            var exists = await _context.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.UserId == userId);
            if (exists) return false;

            _context.PostLikes.Add(new PostLike { PostId = postId, UserId = userId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnlikeAsync(int postId, int userId)
        {
            var like = await _context.PostLikes.FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);
            if (like == null) return false;

            _context.PostLikes.Remove(like);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetLikesCountAsync(int postId)
        {
            return await _context.PostLikes.CountAsync(pl => pl.PostId == postId);
        }

        public async Task<bool> HasUserLikedAsync(int postId, int userId)
        {
            return await _context.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.UserId == userId);
        }
    }
}
