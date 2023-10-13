using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;

namespace PROG3050_HMJJ.Models.DataAccess
{
    public class PreferencesContext : DbContext
    {
        public DbSet<Preferences> Preferences { get; set; }


        public DbSet<Platforms> Platforms { get; set; }


        public DbSet<Genres> Genres { get; set; }


        public DbSet<Languages> Languages { get; set; }


        public PreferencesContext(DbContextOptions<PreferencesContext> options) : base(options)
        {

        }
    }
}
