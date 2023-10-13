using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG3050_HMJJ.Areas.Member.Models;

namespace PROG3050_HMJJ.Models.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext
    {
        public DbSet<Profiles> Profiles { get; set; }

        public GameStoreDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
