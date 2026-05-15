using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static TravelBuddy.GCommon.ValidationConstants.NotificationConstants;

namespace TravelBuddy.Data.Models
{
    public class Notification
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(MessageMaxLength)]
        [Comment("The content of the notification message.")]
        public string Message { get; set; } = null!;

        [Required]
        [Comment("The date and time when the notification was sent.")]
        public DateTime SentOn { get; set; }

        [Required]
        [Comment("Indicates whether the notification has been read by the user.")]
        public bool IsRead { get; set; }


        [Required]
        [ForeignKey(nameof(User))]
        [Comment("Id of the user who received the notification.")]
        public Guid UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
