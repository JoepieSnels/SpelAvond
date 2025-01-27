using Azure.Core.Pipeline;
using IndividueleCSharpProject.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace IndividueleCSharpProject.Infrastructure
{
    public class GamenightDBContext : DbContext
    {
        public DbSet<Games> GamesDb { get; set; }
        public DbSet<GameNight> GameNights { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Person> Persons { get; set; }

        public const string SQL_Schema = "Gamenight";

        public GamenightDBContext(DbContextOptions<GamenightDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); // Zorg ervoor dat Identity configuraties worden toegepast.

            modelBuilder.HasDefaultSchema(SQL_Schema);

            // Jouw bestaande configuratie voor entiteiten zoals Games, GameNights, enz.
            modelBuilder.Entity<Games>()
                .HasKey(g => g.gameId);
            modelBuilder.Entity<Games>()
                .Property(g => g.gameId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GameNight>()
                .HasKey(gn => gn.gameNightId);
            modelBuilder.Entity<GameNight>()
                .Property(gn => gn.gameNightId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Person>()
                .HasKey(p => p.personId);
            modelBuilder.Entity<Person>()
                .Property(p => p.personId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Review>()
                .HasKey(r => r.reviewId);
            modelBuilder.Entity<Review>()
                .Property(r => r.reviewId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.email)
                .IsUnique(); 

            // Configuratie van de relaties
            modelBuilder.Entity<GameNight>()
                .HasMany(gn => gn.players)
                .WithMany(p => p.gameNights)
                .UsingEntity("GamenightPlayers",
                    l => l.HasOne(typeof(Person)).WithMany().HasForeignKey("personId").HasPrincipalKey(nameof(Person.personId)),
                    r => r.HasOne(typeof(GameNight)).WithMany().HasForeignKey("gameNightId").HasPrincipalKey(nameof(GameNight.gameNightId)),
                    j => j.HasKey("personId", "gameNightId")
                );

            modelBuilder.Entity<GameNight>()
                .HasMany(gn => gn.games)
                .WithMany(g => g.gameNights)
                .UsingEntity("GamenightGames",
                    l => l.HasOne(typeof(Games)).WithMany().HasForeignKey("gameId").HasPrincipalKey(nameof(Games.gameId)),
                    r => r.HasOne(typeof(GameNight)).WithMany().HasForeignKey("gameNightId").HasPrincipalKey(nameof(GameNight.gameNightId)),
                    j => j.HasKey("gameId", "gameNightId")
                );

            modelBuilder.Entity<GameNight>()
                .HasMany(gn => gn.reviews)
                .WithOne(r => r.gameNight)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.reviewer)
                .WithMany(p => p.reviews)
                .HasForeignKey(r => r.reviewerId)
                .OnDelete(DeleteBehavior.Cascade);
            

    // Games
            modelBuilder.Entity<Games>().HasData(
                new Games(1, "Game1", "Description1", Genrè.action, true, "https://i.postimg.cc/c4YrVp3f/Monopoly.jpg", TypeOfGame.boardgame),
                new Games(2, "Game2", "Description2", Genrè.action, true, "https://i.postimg.cc/yYF0FgLq/pesten.jpg", TypeOfGame.cardgame),
                new Games(3, "Game3", "Description3", Genrè.action, true, "https://i.postimg.cc/6ppTyySf/Minecraft.jpg", TypeOfGame.videogame),
                new Games(4, "Game4", "Description4", Genrè.action, true, "https://i.postimg.cc/FsDH5hRd/catan.jpg", TypeOfGame.boardgame)
            );

    // Persons
            modelBuilder.Entity<Person>().HasData(
                new Person(3, "John", "Doe", new DateTime(1990, 01, 01), "john.doe@email.com", "Street1", "City1", "1", Gender.Male, true, true, false, false),
                new Person(2, "Jane", "Doe", new DateTime(1992, 02, 15), "jane.doe@email.com", "Street2", "City2", "2", Gender.Female, true, true, false, false)
            );

    // Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review(1, 5, "Great game night!", 3, 1),
                new Review(2, 4, "Enjoyed the evening!", 2, 2)
            );

    // Relationships (join tables)
            modelBuilder.Entity("GamenightGames").HasData(
                new { gameNightId = 1, gameId = 1 },
                new { gameNightId = 1, gameId = 2 },
                new { gameNightId = 2, gameId = 3 },
                new { gameNightId = 2, gameId = 4 }
            );

            modelBuilder.Entity("GamenightPlayers").HasData(
                new { gameNightId = 1, personId = 3 },
                new { gameNightId = 1, personId = 2 }
            );

        }
    }
}
