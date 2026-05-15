using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TravelBuddy.Data.Models.Enums;

using static TravelBuddy.GCommon.ValidationConstants.BookingCancellationRequestConstants;

namespace TravelBuddy.Data.Models
{
    [PrimaryKey(nameof(UserId), nameof(BookingId))]
    public class BookingCancellationRequest
    {
        [Required]
        [ForeignKey(nameof(User))]
        [Comment("Id of the user who made the cancellation request.")]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Booking))]
        [Comment("Id of the booking associated with the cancellation request.")]
        public Guid BookingId { get; set; }

        [Required]
        public virtual Booking Booking { get; set; } = null!;

        [Required]
        [Comment("Date of the cancellation request.")]
        public DateTime RequestedOn { get; set; }

        [MaxLength(ReasonMaxLength)]
        [Comment("Reason for the cancellation request.")]
        public string? Reason { get; set; }

        [Required]
        [Comment("Status of the cancellation request.")]
        public CancellationRequestStatus Status { get; set; }

        [Comment("Date when the cancellation request was reviewed by an administrator.")]
        public DateTime? ReviewedOn { get; set; }
    }
}
