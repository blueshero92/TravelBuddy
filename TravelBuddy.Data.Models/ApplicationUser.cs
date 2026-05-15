using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static TravelBuddy.GCommon.ValidationConstants.ApplicationUserConstants;

namespace TravelBuddy.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        [PersonalData]
        [MaxLength(FullNameMaxLength)]
        [Comment("The full name of the user.")]
        public string FullName { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; } 
            = new HashSet<Booking>();

        public virtual ICollection<Favorite> Favorites { get; set; }
            = new HashSet<Favorite>();

        public virtual ICollection<BookingCancellationRequest> CancellationRequests { get; set; }
            = new HashSet<BookingCancellationRequest>();

        public virtual ICollection<Notification> Notifications { get; set; }
            = new HashSet<Notification>();
    }
}
