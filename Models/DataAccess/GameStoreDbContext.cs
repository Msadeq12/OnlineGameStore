﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Models.Account;
using PROG3050_HMJJ.Areas.Member.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            RoleManager<IdentityRole> roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string username = "member";
            string password = "Test1$";
            string roleName = "Member";

            // if role doesn't exist, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var user = await userManager.FindByNameAsync(username);

            // if username doesn't exist, create it and add it to role
            if (user == null)
            {
                user = new User { UserName = username, Email = "member@cvgs.com", NormalizedEmail = "MEMBER@CVGS.COM", EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
            else
            {
                var deleteUserResult = await userManager.DeleteAsync(user);
                user = new User { UserName = username, Email = "member@cvgs.com", NormalizedEmail = "MEMBER@CVGS.COM", EmailConfirmed = true };
                var createUserResult = await userManager.CreateAsync(user, password);
                if (deleteUserResult.Succeeded && createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
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


        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
        {
        }
    }
}
