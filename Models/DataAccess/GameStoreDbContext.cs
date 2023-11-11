using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Areas.Member.Models;
using Microsoft.AspNetCore.Identity;


namespace PROG3050_HMJJ.Models.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext<User>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Platforms>().HasData(
                new Platforms { ID = 1, Name = "PS5" },
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
                new Languages { ID = 6, Name = "Hindi" },
                new Languages { ID = 7, Name = "Bengali" },
                new Languages { ID = 8, Name = "Persian" },
                new Languages { ID = 9, Name = "Japanese" },
                new Languages { ID = 10, Name = "Italian" }
            );

            builder.Entity<Countries>().HasData(
                new Countries { ID = 1, Name = "Canada" }
                );

            builder.Entity<Regions>().HasData(
                new Regions { ID = 1, Name = "Alberta", CountriesID = 1 },
                new Regions { ID = 2, Name = "British Columbia", CountriesID = 1 },
                new Regions { ID = 3, Name = "Manitoba", CountriesID = 1 },
                new Regions { ID = 4, Name = "New Brunswick", CountriesID = 1 },
                new Regions { ID = 5, Name = "Newfoundland and Labrador", CountriesID = 1 },
                new Regions { ID = 6, Name = "Northwest Territories", CountriesID = 1 },
                new Regions { ID = 7, Name = "Nova Scotia", CountriesID = 1 },
                new Regions { ID = 8, Name = "Nunavut", CountriesID = 1 },
                new Regions { ID = 9, Name = "Ontario", CountriesID = 1 },
                new Regions { ID = 10, Name = "Prince Edward Island", CountriesID = 1 },
                new Regions { ID = 11, Name = "Quebec", CountriesID = 1 },
                new Regions { ID = 12, Name = "Saskatchewan", CountriesID = 1 },
                new Regions { ID = 13, Name = "Yukon", CountriesID = 1 }
            );
            // ToDo: seed address related data and define cascade behaviour

            #region defineCascades
            builder.Entity<Preferences>()
                .HasOne(u => u.User)
                .WithOne(p => p.Preferences)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Profiles>()
               .HasOne(u => u.User)
               .WithOne(p => p.Profiles)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Addresses>()
               .HasOne(u => u.User)
               .WithOne(a => a.Addresses)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }


        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string adminUsername = "admin";
            string adminPassword = "Test1$";
            string adminRoleName = "Admin";


            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(adminRoleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }


            // if admin username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(adminUsername) == null)
            {
                var admin = new User { UserName = adminUsername, Email="admin@cvgs.com", NormalizedEmail="ADMIN@CVGS.COM", EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminRoleName);
                }
            }
        }


        #region seedUserMember
        public static async Task CreateMemberUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();

            // For unit tests only; user is created in unit tests
            var signUpTestMember = await userManager.FindByNameAsync("TestMember");

            if (signUpTestMember != null)
            {
                await userManager.DeleteAsync(signUpTestMember);
            }
        }
        #endregion


        public DbSet<Preferences> Preferences { get; set; }


        public DbSet<Platforms> Platforms { get; set; }


        public DbSet<Genres> Genres { get; set; }


        public DbSet<Languages> Languages { get; set; }


        public DbSet<SelectedPlatforms> SelectedPlatforms { get; set; }


        public DbSet<SelectedGenres> SelectedGenres { get; set; }


        public DbSet<Profiles> Profiles { get; set; }


        public DbSet<Addresses> Addresses { get; set; }

        
        public DbSet<Regions> Regions { get; set; }


        public DbSet<Countries> Countries { get; set; }


        public DbSet<MailingAddresses> MailingAddresses { get; set; }


        public DbSet<ShippingAddresses> ShippingAddresses { get; set; }


        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
        }
    }
}
