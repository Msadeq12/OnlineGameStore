using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Areas.Member.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;


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
                new Countries { ID = 1, Name = "Canada" },
                new Countries { ID = 2, Name = "USA"}
                );

            builder.Entity<Regions>().HasData(
                new Regions { ID = 1, Name = "Alberta", CountryID = 1 },
                new Regions { ID = 2, Name = "British Columbia", CountryID = 1 },
                new Regions { ID = 3, Name = "Manitoba", CountryID = 1 },
                new Regions { ID = 4, Name = "New Brunswick", CountryID = 1 },
                new Regions { ID = 5, Name = "Newfoundland and Labrador", CountryID = 1 },
                new Regions { ID = 6, Name = "Northwest Territories", CountryID = 1 },
                new Regions { ID = 7, Name = "Nova Scotia", CountryID = 1 },
                new Regions { ID = 8, Name = "Nunavut", CountryID = 1 },
                new Regions { ID = 9, Name = "Ontario", CountryID = 1 },
                new Regions { ID = 10, Name = "Prince Edward Island", CountryID = 1 },
                new Regions { ID = 11, Name = "Quebec", CountryID = 1 },
                new Regions { ID = 12, Name = "Saskatchewan", CountryID = 1 },
                new Regions { ID = 13, Name = "Yukon", CountryID = 1 },
                new Regions { ID = 14, Name = "Alabama", CountryID = 2 },
                new Regions { ID = 15, Name = "Alaska", CountryID = 2 },
                new Regions { ID = 16, Name = "American Samoa", CountryID = 2 },
                new Regions { ID = 17, Name = "Arizona", CountryID = 2 },
                new Regions { ID = 18, Name = "Arkansas", CountryID = 2 },
                new Regions { ID = 19, Name = "California", CountryID = 2 },
                new Regions { ID = 20, Name = "Colorado", CountryID = 2 },
                new Regions { ID = 21, Name = "Connecticut", CountryID = 2 },
                new Regions { ID = 22, Name = "Delaware", CountryID = 2 },
                new Regions { ID = 23, Name = "Federated States of Micronesia", CountryID = 2 },
                new Regions { ID = 24, Name = "Florida", CountryID = 2 },
                new Regions { ID = 25, Name = "Georgia", CountryID = 2 },
                new Regions { ID = 26, Name = "Guam", CountryID = 2 },
                new Regions { ID = 27, Name = "Hawaii", CountryID = 2 },
                new Regions { ID = 28, Name = "Idaho", CountryID = 2 },
                new Regions { ID = 29, Name = "Illinois", CountryID = 2 },
                new Regions { ID = 30, Name = "Indiana", CountryID = 2 },
                new Regions { ID = 31, Name = "Iowa", CountryID = 2 },
                new Regions { ID = 32, Name = "Kansas", CountryID = 2 },
                new Regions { ID = 33, Name = "Kentucky", CountryID = 2 },
                new Regions { ID = 34, Name = "Louisiana", CountryID = 2 },
                new Regions { ID = 35, Name = "Maine", CountryID = 2 },
                new Regions { ID = 36, Name = "Marshall Islands", CountryID = 2 },
                new Regions { ID = 37, Name = "Maryland", CountryID = 2 },
                new Regions { ID = 38, Name = "Massachusetts", CountryID = 2 },
                new Regions { ID = 39, Name = "Michigan", CountryID = 2 },
                new Regions { ID = 40, Name = "Minnesota", CountryID = 2 },
                new Regions { ID = 41, Name = "Mississippi", CountryID = 2 },
                new Regions { ID = 42, Name = "Missouri", CountryID = 2 },
                new Regions { ID = 43, Name = "Montana", CountryID = 2 },
                new Regions { ID = 44, Name = "Nebraska", CountryID = 2 },
                new Regions { ID = 45, Name = "Nevada", CountryID = 2 },
                new Regions { ID = 46, Name = "New Hampshire", CountryID = 2 },
                new Regions { ID = 47, Name = "New Jersey", CountryID = 2 },
                new Regions { ID = 48, Name = "New Mexico", CountryID = 2 },
                new Regions { ID = 49, Name = "New York", CountryID = 2 },
                new Regions { ID = 50, Name = "North Carolina", CountryID = 2 },
                new Regions { ID = 51, Name = "North Dakota", CountryID = 2 },
                new Regions { ID = 52, Name = "Northern Mariana Islands", CountryID = 2 },
                new Regions { ID = 53, Name = "Ohio", CountryID = 2 },
                new Regions { ID = 54, Name = "Oklahoma", CountryID = 2 },
                new Regions { ID = 55, Name = "Oregon", CountryID = 2 },
                new Regions { ID = 56, Name = "Palau", CountryID = 2 },
                new Regions { ID = 57, Name = "Pennsylvania", CountryID = 2 },
                new Regions { ID = 58, Name = "Puerto Rico", CountryID = 2 },
                new Regions { ID = 59, Name = "Rhode Island", CountryID = 2 },
                new Regions { ID = 60, Name = "South Carolina", CountryID = 2 },
                new Regions { ID = 61, Name = "South Dakota", CountryID = 2 },
                new Regions { ID = 62, Name = "Tennessee", CountryID = 2 },
                new Regions { ID = 63, Name = "Texas", CountryID = 2 },
                new Regions { ID = 64, Name = "U.S. Minor Outlying Islands", CountryID = 2 },
                new Regions { ID = 65, Name = "U.S. Virgin Islands", CountryID = 2 },
                new Regions { ID = 66, Name = "Utah", CountryID = 2 },
                new Regions { ID = 67, Name = "Vermont", CountryID = 2 },
                new Regions { ID = 68, Name = "Virginia", CountryID = 2 },
                new Regions { ID = 69, Name = "Washington", CountryID = 2 },
                new Regions { ID = 70, Name = "West Virginia", CountryID = 2 },
                new Regions { ID = 71, Name = "Wisconsin", CountryID = 2 },
                new Regions { ID = 72, Name = "Wyoming", CountryID = 2 }
            );


            #region defineCascades
            builder.Entity<User>()
                .HasOne(u => u.Preferences)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<User>()
               .HasOne(u => u.Profiles)
               .WithOne(p => p.User)
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


        public DbSet<Address> Address { get; set; }


        public DbSet<Countries> Countries { get; set; }


        public DbSet<Regions> Regions { get; set; }


        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
        }
    }
}
