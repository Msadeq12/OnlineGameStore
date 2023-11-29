using GameService.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameService.Entities
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

            modelBuilder.Entity<Platform>()
                .HasMany(g => g.Games)
                .WithOne(g => g.Platform);

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

            modelBuilder.Entity<Platform>().HasData(
                new Platform { PlatformID = 1, Name = "PS5" },
                new Platform { PlatformID = 2, Name = "Xbox" },
                new Platform { PlatformID = 3, Name = "PC" },
                new Platform { PlatformID = 4, Name = "Android" },
                new Platform { PlatformID = 5, Name = "iOS" }
                );

            modelBuilder.Entity<Game>().HasData(
                new Game { gameID = 1, Title = "Heroes in Action", Description = "A game about heroes in action", Price = 10.99m, GenreID = 1, OrderType = "Physical", isPurchased = false, Publisher = "Petroglyph", ReleaseYear = 2005, PlatformID = 2 },
                new Game { gameID = 2, Title = "Adventures in the Black Forest", Description = "A game about adventure in the forest", Price = 9.99m, GenreID = 2, OrderType = "Digital", isPurchased = false, Publisher = "Inc Mania", ReleaseYear = 2012, PlatformID = 3 },
                new Game { gameID = 3, Title = "Escape the City", Description = "An RPG game in the city", Price = 8.99m, GenreID = 3, OrderType = "Physical", Publisher = "Kronos Studios", isPurchased = false, ReleaseYear = 2021, PlatformID = 4 },
                new Game { gameID = 4, Title = "Heroes in Action", Description = "A game about heroes in action", Price = 10.99m, GenreID = 1, OrderType = "Digital", isPurchased = false, Publisher = "Petroglyph", ReleaseYear = 2005, PlatformID = 1 }
                );

            /*modelBuilder.Entity<GameDTO>().HasData(

                new GameDTO { ID = 1, Title = "Heroes in Action", Description = "A game about heroes in action", Price = 10.99m, GameGenre = "Action", GamePlatform = "Xbox", Publisher = "Petroglyph", ReleaseYear = 2005 },
                new GameDTO { ID = 2, Title = "Adventures in the Black Forest", Description = "A game about adventure in the forest", Price = 9.99m, GameGenre = "Adventure", GamePlatform = "Xbox", Publisher = "Inc Mania", ReleaseYear = 2012 },
                new GameDTO { ID = 3, Title = "Escape the City", Description = "An RPG game in the city", Price = 8.99m, GameGenre = "Action", GamePlatform = "RPG", Publisher = "Kronos Studios", ReleaseYear = 2021 },
                new GameDTO { ID = 4, Title = "Heroes in Action", Description = "A game about heroes in action", Price = 11.99m, GameGenre = "Action", GamePlatform = "Xbox", Publisher = "Petroglyph", ReleaseYear = 2005 }

                );*/
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        /*public DbSet<GameDTO> GameDataTransferObjects { get; set; }*/
    }
}
