using Sim_Forum.Models;
using Thread = Sim_Forum.Models.Thread;



namespace Sim_Forum.Data
{
    public static class DbInitializer
    {
        public static void SeedUsers(ForumContext context)
        {
            if (context.Users.Any())
                return; // déjà seedé

            var users = new List<User>
            {
                new User
                {
                    Username = "cedric",
                    Email = "cedric@forum.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password_cedric"),
                    Role = "admin",
                    AvatarUrl = "/default-avatar.png",
                    Signature = "Administrateur du forum",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Username = "john",
                    Email = "john@forum.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password_john"),
                    Role = "user",
                    AvatarUrl = "/default-avatar.png",
                    Signature = "Membre passionné",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    

        public static void SeedForum(ForumContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "Général", Description = "Discussions générales" },
                new Category { Name = "Aide", Description = "Questions et support" },
                new Category { Name = "Développement", Description = "Code, projets, astuces" }
            };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            var users = context.Users.ToList();
            if (!users.Any()) return; // Assure-toi qu'il y a déjà des users

            // --- Threads ---
            if (!context.Threads.Any())
            {
                var threads = new List<Thread>
            {
                new Thread { Title = "Bienvenue sur le forum", CategoryId = 1, UserId = users[0].Id },
                new Thread { Title = "Problème avec mon code", CategoryId = 3, UserId = users[1].Id },
                new Thread { Title = "Comment poser une question ?", CategoryId = 2, UserId = users[0].Id }
            };
                context.Threads.AddRange(threads);
                context.SaveChanges();
            }

            var threadsList = context.Threads.ToList();

            // --- Posts ---
            if (!context.Posts.Any())
            {
                var posts = new List<Post>
            {
                new Post { Content = "Salut à tous ! Bienvenue ici.", ThreadId = threadsList[0].Id, UserId = users[0].Id },
                new Post { Content = "Merci pour l'accueil !", ThreadId = threadsList[0].Id, UserId = users[1].Id },
                new Post { Content = "J'ai un bug dans ma méthode, quelqu'un peut aider ?", ThreadId = threadsList[1].Id, UserId = users[1].Id },
                new Post { Content = "Pour poser une question claire, mets un titre précis et un exemple de code.", ThreadId = threadsList[2].Id, UserId = users[0].Id }
            };
                context.Posts.AddRange(posts);
                context.SaveChanges();
            }

            // --- Tags ---
            if (!context.Tags.Any())
            {
                var tags = new List<Tag>
            {
                new Tag { Name = "Important" },
                new Tag { Name = "Bug" },
                new Tag { Name = "Question" },
                new Tag { Name = "Astuce" }
            };
                context.Tags.AddRange(tags);
                context.SaveChanges();
            }

            // --- PostTags ---
            if (!context.PostTags.Any())
            {
                var posts = context.Posts.ToList();
                var tags = context.Tags.ToList();
                context.PostTags.AddRange(new List<PostTag>
            {
                new PostTag { PostId = posts[0].Id, TagId = tags[0].Id },
                new PostTag { PostId = posts[2].Id, TagId = tags[1].Id },
                new PostTag { PostId = posts[2].Id, TagId = tags[2].Id }
            });
                context.SaveChanges();
            }

            // --- Attachments ---
            if (!context.Attachments.Any())
            {
                var posts = context.Posts.ToList();
                context.Attachments.AddRange(new List<Attachment>
            {
                new Attachment {FileName = "image1.png"  ,FileUrl = "https://via.placeholder.com/150", PostId = posts[0].Id },
                new Attachment {FileName = "image2.png",FileUrl = "https://via.placeholder.com/200", PostId = posts[2].Id }
            });
                context.SaveChanges();
            }

            // --- PostLikes ---
            if (!context.PostLikes.Any())
            {
                var posts = context.Posts.ToList();
                context.PostLikes.AddRange(new List<PostLike>
            {
                new PostLike { PostId = posts[0].Id, UserId = users[1].Id },
                new PostLike { PostId = posts[2].Id, UserId = users[0].Id }
            });
                context.SaveChanges();
            }
        }
    }
}

