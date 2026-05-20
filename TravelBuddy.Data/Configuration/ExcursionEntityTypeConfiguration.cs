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
                StartDate = new DateTime(2024, 7, 1),
                EndDate = new DateTime(2024, 7, 7),
                Price = 1500.00m,
                Capacity = 20,
                ImageUrl = "https://example.com/images/beach-paradise.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("7f763f9f-152f-46b6-95cc-7ca4cb907a9b"),
                Title = "Mountain Adventure",
                Destination = "Swiss Alps",
                StartDate = new DateTime(2024, 8, 15),
                EndDate = new DateTime(2024, 8, 22),
                Price = 1200.00m,
                Capacity = 15,
                ImageUrl = "https://example.com/images/mountain-adventure.jpg"
            },
            new Excursion
            {
                Id = Guid.Parse("178a0c9f-75b7-4c90-9e72-53922dfa72e4"),
                Title = "City Exploration",
                Destination = "New York City",
                StartDate = new DateTime(2024, 9, 10),
                EndDate = new DateTime(2024, 9, 17),
                Price = 1000.00m,
                Capacity = 25,
                ImageUrl = "https://example.com/images/city-exploration.jpg"
            }
        };
    }
}
