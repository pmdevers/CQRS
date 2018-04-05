using Microsoft.EntityFrameworkCore;

namespace PMDEvers.CQRS.EntityFramework
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasKey(x => x.Id);
            modelBuilder.Entity<Event>().Property(x => x.AggregateId).IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.Data).IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.Version).IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.TimeStamp).IsRequired();

            modelBuilder.Entity<Event>().HasIndex(x => new { x.AggregateId, x.Version }).IsUnique();
        }
    }
}
