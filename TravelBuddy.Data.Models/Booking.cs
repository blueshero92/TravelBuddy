using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TravelBuddy.Data.Models.Enums;

namespace TravelBuddy.Data.Models
{
    
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        [Comment("Id of the user who made the booking.")]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Excursion))]
        [Comment("Id of the excursion associated with the booking.")]
        public Guid ExcursionId { get; set; }

        [Required]
        public virtual Excursion Excursion { get; set; } = null!;

        [Required]
        [Comment("Date when the booking was made.")]
        public DateTime BookedOn { get; set; }

        [Required]
        [Comment("Status of the booking.")]
        public Status Status { get; set; }

        public virtual BookingCancellationRequest? CancellationRequest { get; set; }
    }
}
