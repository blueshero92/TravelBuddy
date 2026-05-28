using System.ComponentModel.DataAnnotations;
using static TravelBuddy.GCommon.ValidationConstants.ExcursionConstants;

namespace TravelBuddy.ViewModels.Excursion
{
    public class ExcursionInputModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(TitleMinLength, ErrorMessage = "Title must be at least {1} characters long.")]
        [MaxLength(TitleMaxLength, ErrorMessage = "Title cannot exceed {1} characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Destination is required.")]
        [MinLength(DestinationMinLength, ErrorMessage = "Destination must be at least {1} characters long.")]
        [MaxLength(DestinationMaxLength, ErrorMessage = "Destination cannot exceed {1} characters.")]
        public string Destination { get; set; } = null!;

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range((double)PriceMinValue, (double)PriceMaxValue, ErrorMessage = "Price must be between {1} and {2}.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(CapacityMinValue, CapacityMaxValue, ErrorMessage = "Capacity must be between {1} and {2}.")]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
