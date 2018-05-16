using System.Data.Entity;

namespace Lab1.Model
{
    public class EventContext : DbContext
    {
        public EventContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new Initializer());
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
