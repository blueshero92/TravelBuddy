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
                ImageUrl = "/images/excursions/maldives-beach.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                Title = "Mountain Adventure",
                Destination = "Swiss Alps, Switzerland",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 8, 15), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 8, 22), DateTimeKind.Utc),
                Price = 1200.00m,
                Capacity = 15,
                ImageUrl = "/images/excursions/alps-mountains.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                Title = "City Exploration",
                Destination = "New York City, USA",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 9, 10), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 9, 17), DateTimeKind.Utc),
                Price = 1000.00m,
                Capacity = 25,
                ImageUrl = "/images/excursions/nyc.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("0421801c-e358-45e8-a50b-90b0dbf4b220"),
                Title = "The Magic Of Florence",
                Destination = "Florence, Italy",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 8, 1), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 8, 7), DateTimeKind.Utc),
                Price = 1000.00m,
                Capacity = 35,
                ImageUrl = "/images/excursions/florence.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("1d1c7ddb-d496-463c-8be1-c5b060e80ea6"),
                Title = "Summer in Kavala",
                Destination = "Kavala, Greece",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 8, 15), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 8, 22), DateTimeKind.Utc),
                Price = 800.00m,
                Capacity = 15,
                ImageUrl = "/images/excursions/kavala-beach.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("6ae52e99-8f0b-4648-888b-e87302dbfc09"),
                Title = "Disneyland",
                Destination = "Paris, France",
                StartDate = DateTime.SpecifyKind(new DateTime(2026, 9, 10), DateTimeKind.Utc),
                EndDate = DateTime.SpecifyKind(new DateTime(2026, 9, 17), DateTimeKind.Utc),
                Price = 2000.00m,
                Capacity = 25,
                ImageUrl = "/images/excursions/disneyland.jpg"
            }
        };
    }
}
