using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Areas.Member.Models;
using System.Reflection.Emit;

namespace PROG3050_HMJJ.Models.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Platforms>().HasData(
                new Platforms { ID = 1, Name = "PS5"},
                new Platforms { ID = 2, Name = "Xbox" },
                new Platforms { ID = 3, Name = "PC" },
                new Platforms { ID = 4, Name = "Android" },
                new Platforms { ID = 5, Name = "iOS" }
                );

            builder.Entity<Genres>().HasData(
                new Genres { ID = 1, Name = "Action" },
                new Genres { ID = 2, Name = "Adventure" },
                new Genres { ID = 3, Name = "RPG" },
                new Genres { ID = 4, Name = "Simulation" },
                new Genres { ID = 5, Name = "Strategy" },
                new Genres { ID = 6, Name = "Sports" },
                new Genres { ID = 7, Name = "Puzzle" },
                new Genres { ID = 8, Name = "Idle" },
                new Genres { ID = 9, Name = "Casual" }
                );

            builder.Entity<Languages>().HasData(

                new Languages { ID = 1, Name = "English" },
                new Languages { ID = 2, Name = "French" },
                new Languages { ID = 3, Name = "German" },
                new Languages { ID = 4, Name = "Swedish" },
                new Languages { ID = 5, Name = "Spanish" },
                new Languages { ID = 6, Name = "Hindi"},
                new Languages { ID = 7, Name = "Bengali" },
                new Languages { ID = 8, Name = "Persian" },
                new Languages { ID = 9, Name = "Japanese" },
                new Languages { ID = 10, Name = "Italian" }
                );
        }

        public DbSet<Preferences> Preferences { get; set; }


        public DbSet<Platforms> Platforms { get; set; }


        public DbSet<Genres> Genres { get; set; }


        public DbSet<Languages> Languages { get; set; }


        public DbSet<Profiles> Profiles { get; set; }


        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {

        }
    }
}
