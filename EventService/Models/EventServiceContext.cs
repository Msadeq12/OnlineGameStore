using Microsoft.EntityFrameworkCore;

namespace EventService.Models
{
    public class EventServiceContext : DbContext
    {
        public EventServiceContext(DbContextOptions<EventServiceContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event { eventID = 1, Name = "The Big Event", Location = "The Big City", Description = "A big event in the big city", Date = new DateTime(2021, 12, 25) },
                new Event { eventID = 2, Name = "The Small Event", Location = "The Small City", Description = "A small event in the small city", Date = new DateTime(2021, 12, 26) }

                );
        }
                                                 

        public DbSet<Event> Events { get; set; }
    }
    
    
}
