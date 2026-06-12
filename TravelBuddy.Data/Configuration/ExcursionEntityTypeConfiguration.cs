using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBuddy.Data.Models;

namespace TravelBuddy.Data.Configuration
{
    public class ExcursionEntityTypeConfiguration : IEntityTypeConfiguration<Excursion>
    {
        public void Configure(EntityTypeBuilder<Excursion> entity)
        {
            entity.HasData(excursions);
        }

        private readonly IEnumerable<Excursion> excursions = new List<Excursion>
        {
            new Excursion
            {
                Id = Guid.Parse("c31c6d7f-07f7-4b33-8330-e45d3c5e3819"),
                Title = "Beach Paradise",
                Destination = "Maldives",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 7, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 7, 7), DateTimeKind.Utc),
                Price = 1500.00m,
                Capacity = 20,
                ImageUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=600&auto=format&fit=crop"
            },
            new Excursion
            {
                Id = Guid.Parse("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                Title = "Mountain Adventure",
                Destination = "Swiss Alps",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 8, 15), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 8, 22), DateTimeKind.Utc),
                Price = 1200.00m,
                Capacity = 15,
                ImageUrl = "https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?w=600&auto=format&fit=crop"
            },
            new Excursion
            {
                Id = Guid.Parse("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                Title = "City Exploration",
                Destination = "New York City",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 9, 10), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 9, 17), DateTimeKind.Utc),
                Price = 1000.00m,
                Capacity = 25,
                ImageUrl = "https://images.unsplash.com/photo-1499092346589-b9b6be3e94b2?w=600&auto=format&fit=crop"
            }
        };
    }
}
