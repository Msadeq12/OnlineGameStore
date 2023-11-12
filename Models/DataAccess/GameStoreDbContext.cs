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
                new Countries { ID = 1, Name = "Canada" },
                new Countries { ID = 2, Name = "US" }
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
                new Regions { ID = 13, Name = "Yukon", CountriesID = 1 },
                new Regions { ID = 14, Name = "Alabama", CountriesID = 2 },
                new Regions { ID = 15, Name = "Alaska", CountriesID = 2 },
                new Regions { ID = 16, Name = "American Samoa", CountriesID = 2 },
                new Regions { ID = 17, Name = "Arizona", CountriesID = 2 },
                new Regions { ID = 18, Name = "Arkansas", CountriesID = 2 },
                new Regions { ID = 19, Name = "California", CountriesID = 2 },
                new Regions { ID = 20, Name = "Colorado", CountriesID = 2 },
                new Regions { ID = 21, Name = "Connecticut", CountriesID = 2 },
                new Regions { ID = 22, Name = "Delaware", CountriesID = 2 },
                new Regions { ID = 23, Name = "Federated States of Micronesia", CountriesID = 2 },
                new Regions { ID = 24, Name = "Florida", CountriesID = 2 },
                new Regions { ID = 25, Name = "Georgia", CountriesID = 2 },
                new Regions { ID = 26, Name = "Guam", CountriesID = 2 },
                new Regions { ID = 27, Name = "Hawaii", CountriesID = 2 },
                new Regions { ID = 28, Name = "Idaho", CountriesID = 2 },
                new Regions { ID = 29, Name = "Illinois", CountriesID = 2 },
                new Regions { ID = 30, Name = "Indiana", CountriesID = 2 },
                new Regions { ID = 31, Name = "Iowa", CountriesID = 2 },
                new Regions { ID = 32, Name = "Kansas", CountriesID = 2 },
                new Regions { ID = 33, Name = "Kentucky", CountriesID = 2 },
                new Regions { ID = 34, Name = "Louisiana", CountriesID = 2 },
                new Regions { ID = 35, Name = "Maine", CountriesID = 2 },
                new Regions { ID = 36, Name = "Marshall Islands", CountriesID = 2 },
                new Regions { ID = 37, Name = "Maryland", CountriesID = 2 },
                new Regions { ID = 38, Name = "Massachusetts", CountriesID = 2 },
                new Regions { ID = 39, Name = "Michigan", CountriesID = 2 },
                new Regions { ID = 40, Name = "Minnesota", CountriesID = 2 },
                new Regions { ID = 41, Name = "Mississippi", CountriesID = 2 },
                new Regions { ID = 42, Name = "Missouri", CountriesID = 2 },
                new Regions { ID = 43, Name = "Montana", CountriesID = 2 },
                new Regions { ID = 44, Name = "Nebraska", CountriesID = 2 },
                new Regions { ID = 45, Name = "Nevada", CountriesID = 2 },
                new Regions { ID = 46, Name = "New Hampshire", CountriesID = 2 },
                new Regions { ID = 47, Name = "New Jersey", CountriesID = 2 },
                new Regions { ID = 48, Name = "New Mexico", CountriesID = 2 },
                new Regions { ID = 49, Name = "New York", CountriesID = 2 },
                new Regions { ID = 50, Name = "North Carolina", CountriesID = 2 },
                new Regions { ID = 51, Name = "North Dakota", CountriesID = 2 },
                new Regions { ID = 52, Name = "Northern Mariana Islands", CountriesID = 2 },
                new Regions { ID = 53, Name = "Ohio", CountriesID = 2 },
                new Regions { ID = 54, Name = "Oklahoma", CountriesID = 2 },
                new Regions { ID = 55, Name = "Oregon", CountriesID = 2 },
                new Regions { ID = 56, Name = "Palau", CountriesID = 2 },
                new Regions { ID = 57, Name = "Pennsylvania", CountriesID = 2 },
                new Regions { ID = 58, Name = "Puerto Rico", CountriesID = 2 },
                new Regions { ID = 59, Name = "Rhode Island", CountriesID = 2 },
                new Regions { ID = 60, Name = "South Carolina", CountriesID = 2 },
                new Regions { ID = 61, Name = "South Dakota", CountriesID = 2 },
                new Regions { ID = 62, Name = "Tennessee", CountriesID = 2 },
                new Regions { ID = 63, Name = "Texas", CountriesID = 2 },
                new Regions { ID = 64, Name = "U.S. Minor Outlying Islands", CountriesID = 2 },
                new Regions { ID = 65, Name = "U.S. Virgin Islands", CountriesID = 2 },
                new Regions { ID = 66, Name = "Utah", CountriesID = 2 },
                new Regions { ID = 67, Name = "Vermont", CountriesID = 2 },
                new Regions { ID = 68, Name = "Virginia", CountriesID = 2 },
                new Regions { ID = 69, Name = "Washington", CountriesID = 2 },
                new Regions { ID = 70, Name = "West Virginia", CountriesID = 2 },
                new Regions { ID = 71, Name = "Wisconsin", CountriesID = 2 },
                new Regions { ID = 72, Name = "Wyoming", CountriesID = 2 }
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
            builder.Entity<Reviews>()
        .Property(r => r.CommentId)
        .HasDefaultValueSql("NEWID()");
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
        public DbSet<Reviews> Reviews { get; set; }


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
