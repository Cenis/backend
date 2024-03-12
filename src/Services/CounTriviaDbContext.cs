using CounTrivia.Entities;
using Microsoft.EntityFrameworkCore;

namespace CounTrivia.Services;

public class CounTriviaDbContext : DbContext
{
    public virtual DbSet<Challenge> Challenges { get; set; } = default!;
    public virtual DbSet<Country> Countries { get; set; } = default!;

    public CounTriviaDbContext(DbContextOptions<CounTriviaDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
        var config = configBuilder.Build();
        var connectionString = config.GetConnectionString("CounTriviaMariaDbInDocker");

        Console.WriteLine($"ConnectionString: {connectionString}");

        if (!string.IsNullOrEmpty(connectionString))
        {
            optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 10)));
        }
        else
        {
            optionsBuilder.UseInMemoryDatabase("CounTrivia");
        }
    }
}