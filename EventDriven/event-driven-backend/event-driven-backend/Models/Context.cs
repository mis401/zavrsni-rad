using Microsoft.EntityFrameworkCore;

namespace event_driven_backend.Models
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<UserCommunity> UserCommunities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Calendar> Calendars { get; set; }

        public DbSet<Document> Documents { get; set; }
        public Context(DbContextOptions options) : base(options) { 
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(e => e.Color)
                .HasDefaultValue(EventTheme.WHITE);
            modelBuilder.Entity<Community>()
                .Property(c => c.CreatedAt)
                .HasDefaultValue(DateTime.Now.ToUniversalTime());
            modelBuilder.Entity<User>()
                .HasIndex(User => User.Email)
                .IsUnique(true);
        }
    }

}
