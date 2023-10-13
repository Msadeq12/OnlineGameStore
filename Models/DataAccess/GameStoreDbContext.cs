using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;

namespace PROG3050_HMJJ.Models.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext
    {

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
