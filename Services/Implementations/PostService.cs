using Microsoft.EntityFrameworkCore;
using Sim_Forum.Data;
using Sim_Forum.DTOs.Forum.Threads;
using Sim_Forum.DTOs.Users;
using Sim_Forum.Models;
using Sim_Forum.Services.Interfaces;

namespace Sim_Forum.Services.Implementations
{
        public class PostService : IPostService
        {
            private readonly ForumContext _context;

            public PostService(ForumContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<PostDto>> GetByThreadAsync(int threadId)
            {
                return await _context.Posts
                    .Where(p => p.ThreadId == threadId)
                    .Select(p => new PostDto
                    {
                        Id = p.Id,
                        Content = p.Content,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        ThreadId = p.ThreadId,
                        User = new UserSummaryDto
                        {
                            Id = p.User.Id,
                            Username = p.User.Username,
                            AvatarUrl = p.User.AvatarUrl
                        }
                    })
                    .ToListAsync();
            }

            public async Task<PostDto?> GetByIdAsync(int id)
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null) return null;

                return new PostDto
                {
                    Id = post.Id,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt,
                    ThreadId = post.ThreadId,
                    User = new UserSummaryDto
                    {
                        Id = post.User.Id,
                        Username = post.User.Username,
                        AvatarUrl = post.User.AvatarUrl
                    }
                };
            }

            public async Task<PostDto> CreateAsync(CreatePostDto dto, int userId)
            {
                var post = new Post
                {
                    Content = dto.Content,
                    ThreadId = dto.ThreadId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

            if (dto.TagIds != null && dto.TagIds.Any())
            {
                foreach (var tagId in dto.TagIds)
                {
                    _context.PostTags.Add(new PostTag
                    {
                        PostId = post.Id,
                        TagId = tagId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return new PostDto
                {
                    Id = post.Id,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt,
                    ThreadId = post.ThreadId,
                User = new UserSummaryDto
                {
                    Id = post.User.Id,
                    Username = post.User.Username,
                    AvatarUrl = post.User.AvatarUrl
                }
            };
            }

            public async Task<bool> UpdateAsync(int id, UpdatePostDto dto, int userId)
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null || post.UserId != userId) return false;

                if (!string.IsNullOrEmpty(dto.Content))
                {
                    post.Content = dto.Content;
                    post.UpdatedAt = DateTime.UtcNow;
                }

            if (dto.TagIds != null)
            {
                post.PostTags.Clear();
                foreach (var tagId in dto.TagIds)
                {
                    post.PostTags.Add(new PostTag { PostId = post.Id, TagId = tagId });
                }
            }

            await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteAsync(int id, int userId)
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null || post.UserId != userId) return false;

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    
}
