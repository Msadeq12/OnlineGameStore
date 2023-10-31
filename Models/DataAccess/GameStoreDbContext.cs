using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Areas.Member.Models;
using Microsoft.AspNetCore.Identity;


namespace PROG3050_HMJJ.Models.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext<User>
    {
        #region seedMiscData
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region seedPlatforms
            builder.Entity<Platforms>().HasData(
                new Platforms { ID = 1, Name = "PS5" },
                new Platforms { ID = 2, Name = "Xbox" },
                new Platforms { ID = 3, Name = "PC" },
                new Platforms { ID = 4, Name = "Android" },
                new Platforms { ID = 5, Name = "iOS" }
            );
            #endregion

            #region seedGenres
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
            #endregion

            #region seedLanguages
            builder.Entity<Languages>().HasData(
                new Languages { ID = 1, Name = "English" },
                new Languages { ID = 2, Name = "French" },
                new Languages { ID = 3, Name = "German" },
                new Languages { ID = 4, Name = "Swedish" },
                new Languages { ID = 5, Name = "Spanish" },
                new Languages { ID = 6, Name = "Hindi" },
                new Languages { ID = 7, Name = "Bengali" },
                new Languages { ID = 8, Name = "Persian" },
                new Languages { ID = 9, Name = "Japanese" },
                new Languages { ID = 10, Name = "Italian" }
            );
            #endregion

            #region seedEvents
            builder.Entity<Events>().HasData(
                new Events { ID = 1, Name = "The Big Event", Location = "The Big City", Description = "A big event in the big city", Date = new DateTime(2021, 12, 25) },
                new Events { ID = 2, Name = "The Small Event", Location = "The Small City", Description = "A small event in the small city", Date = new DateTime(2021, 12, 26) }
            );
            #endregion
        }
        #endregion


        #region seedUserAdmin
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Test1$";
            string roleName = "Admin";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username, Email="admin@cvgs.com", NormalizedEmail="ADMIN@CVGS.COM", EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
        #endregion


        #region tables
        public DbSet<Preferences> Preferences { get; set; }


        public DbSet<Platforms> Platforms { get; set; }


        public DbSet<Genres> Genres { get; set; }


        public DbSet<Languages> Languages { get; set; }


        public DbSet<SelectedPlatforms> SelectedPlatforms { get; set; }


        public DbSet<SelectedGenres> SelectedGenres { get; set; }


        public DbSet<Profiles> Profiles { get; set; }


        public DbSet<Games> Games { get; set; }
        #endregion


        #region constructor
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {

        }
        #endregion
    }
}
