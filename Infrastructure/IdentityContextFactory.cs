using IndividueleCSharpProject.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
{
    public IdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

        // Configureer de configuratie om de connection string uit appsettings.json te halen
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  // Zorg ervoor dat de locatie van appsettings.json klopt
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Gebruik de juiste connection string
        optionsBuilder.UseSqlServer(connectionString);

        return new IdentityContext(optionsBuilder.Options);
    }
}
