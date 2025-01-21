using Microsoft.EntityFrameworkCore;
using EveBackend.Models;

namespace EveBackend.Data
{
    public class EveBackendDbContext : DbContext
    {
        public EveBackendDbContext(DbContextOptions<EveBackendDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Tech Conference 2025",
                    Description = "A conference about the latest in tech.",
                    Date = new DateTime(2025, 5, 1),
                    Location = "Berlin",
                    MaxAttendees = 300,
                    Attendees = new List<string>()
                },
                new Event
                {
                    Id = 2,
                    Name = "Community Meetup",
                    Description = "A local meetup for the community.",
                    Date = new DateTime(2025, 6, 15),
                    Location = "Potsdam",
                    MaxAttendees = 100,
                    Attendees = new List<string>()
                }
            );
        }
    }
}
