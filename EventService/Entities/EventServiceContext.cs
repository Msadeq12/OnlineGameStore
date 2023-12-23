using Microsoft.EntityFrameworkCore;

namespace EventService.Entities
{
    public class EventServiceContext : DbContext
    {
        public EventServiceContext(DbContextOptions<EventServiceContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
                                                 

        public DbSet<Event> Events { get; set; }
    }
    
    
}
