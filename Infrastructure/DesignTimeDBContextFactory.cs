using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IndividueleCSharpProject.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GamenightDBContext>
    {
        public GamenightDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GamenightDBContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Haal de verbindingstring op uit je configuratie
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            // Bij ontwerp-time geen UserManager, dus geef deze niet mee
            return new GamenightDBContext(optionsBuilder.Options);
        }
    }
}
