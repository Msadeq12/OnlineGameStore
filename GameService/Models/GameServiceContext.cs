using Microsoft.EntityFrameworkCore;

namespace GameService.Models
{
    public class GameServiceContext : DbContext
    {
        public GameServiceContext(DbContextOptions<GameServiceContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Games)
                .WithOne(g => g.Genre);

            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreID = 1, GenreName = "Action"},
                new Genre { GenreID = 2, GenreName = "Adventure"},
                new Genre { GenreID = 3, GenreName = "RPG"},
                new Genre { GenreID = 4, GenreName = "Simulation"},
                new Genre { GenreID = 5, GenreName = "Strategy"},
                new Genre { GenreID = 6, GenreName = "Sports"},
                new Genre { GenreID = 7, GenreName = "Puzzle"},
                new Genre { GenreID = 8, GenreName = "Idle"},
                new Genre { GenreID = 9, GenreName = "Casual"}
                );

            modelBuilder.Entity<Game>().HasData(
                new Game { gameID = 1, Title = "Heroes in Action", Description = "A game about heroes in action", Price = 10.99m, GenreID = 1, Publisher = "Petroglyph", ReleaseYear = 2005 },
                new Game { gameID = 2, Title = "Adventures in the Black Forest", Description = "A game about adventure in the forest", Price = 9.99m, GenreID = 2, Publisher = "Inc Mania", ReleaseYear = 2012 },
                new Game { gameID = 3, Title = "Escape the City", Description = "An RPG game in the city", Price = 8.99m, GenreID = 3, Publisher = "Kronos Studios", ReleaseYear = 2021 }



                );
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }
    }
}
