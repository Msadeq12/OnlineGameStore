using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PROG3050_HMJJ.Models.DataAccess
{
    public class GameStoreDbContext : IdentityDbContext
    {
        public GameStoreDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
