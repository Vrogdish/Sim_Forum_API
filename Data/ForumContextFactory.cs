using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sim_Forum.Data
{
    public class ForumContextFactory : IDesignTimeDbContextFactory<ForumContext>
    {
        public ForumContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ForumContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Database=SIMFORUM;Username=cedric;Password=241279");
            return new ForumContext(optionsBuilder.Options);
        }
    }
}
