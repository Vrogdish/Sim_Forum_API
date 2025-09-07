using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sim_Forum.Data
{
    public class ForumContextFactory : IDesignTimeDbContextFactory<ForumContext>
    {
        //public ForumContext CreateDbContext(string[] args)
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<ForumContext>();

        //    optionsBuilder.UseNpgsql("Host=localhost;Database=SIMFORUM;Username=cedric;Password=241279#c3dr1c!@s1mfoRuM");
        //    return new ForumContext(optionsBuilder.Options);
        //}

        public ForumContext CreateDbContext(string[] args)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
            var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "SIMFORUM";
            var user = Environment.GetEnvironmentVariable("DB_USER") ?? "cedric";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD")
                           ?? throw new InvalidOperationException("La variable DB_PASSWORD n'est pas définie.");

            // Construire la chaîne de connexion
            var connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";

            var optionsBuilder = new DbContextOptionsBuilder<ForumContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ForumContext(optionsBuilder.Options);
        }
    }
}
