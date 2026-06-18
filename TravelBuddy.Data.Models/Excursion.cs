using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TravelBuddy.GCommon.Constants.ValidationConstants.ExcursionConstants;

namespace TravelBuddy.Data.Models
{
    public class Excursion
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("The title of the excursion.")]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DestinationMaxLength)]
        [Comment("The destination of the excursion.")]
        public string Destination { get; set; } = null!;

        [Required]
        [Comment("Start date of the excursion.")]
        public DateTime StartDate { get; set; }

        [Required]
        [Comment("End date of the excursion.")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = PriceType)]
        [Comment("Price of the excursion.")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Free tourist spots available for the excursion.")]
        public int Capacity { get; set; }

        [Required]
        [Comment("Indicates whether the excursion is active.")]
        public bool IsActive { get; set; } = true;

        [Comment("URL of the excursion image.")]
        public string? ImageUrl { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
            = new HashSet<Booking>();

        public virtual ICollection<Favorite> Favorites { get; set; }
            = new HashSet<Favorite>();
    }
}
