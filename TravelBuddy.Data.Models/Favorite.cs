using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBuddy.Data.Models
{
    [PrimaryKey(nameof(UserId), nameof(ExcursionId))]
    public class Favorite
    {
        [Required]
        [ForeignKey(nameof(User))]
        [Comment("Id of the user who added the excursion to favorites.")]
        public Guid UserId { get; set; }

        public  virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Excursion))]
        [Comment("Id of the excursion that was added to favorites.")]
        public Guid ExcursionId { get; set; }

        public virtual Excursion Excursion { get; set; } = null!;


    }
}
