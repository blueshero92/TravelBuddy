using TravelBuddy.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TravelBuddy.Data
{
    public class TravelBuddyDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public TravelBuddyDbContext(DbContextOptions<TravelBuddyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public virtual DbSet<Excursion> Excursions { get; set; } = null!;

        public virtual DbSet<Booking> Bookings { get; set; } = null!;

        public virtual DbSet<BookingCancellationRequest> BookingCancellationRequests { get; set; } = null!;

        public virtual DbSet<Notification> Notifications { get; set; } = null!;

        public virtual DbSet<Favorite> Favorites { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Convert table names to lowercase.
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()?.ToLower());
            }

            builder.ApplyConfigurationsFromAssembly(typeof(TravelBuddyDbContext).Assembly);
        }
    }
}
