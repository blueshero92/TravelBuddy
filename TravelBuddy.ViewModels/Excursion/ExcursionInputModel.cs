using System.ComponentModel.DataAnnotations;
using static TravelBuddy.GCommon.OutputMessages;
using static TravelBuddy.GCommon.ValidationConstants.ExcursionConstants;

namespace TravelBuddy.ViewModels.Excursion
{
    public class ExcursionInputModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = ExcursionTitleRequired)]
        [MinLength(TitleMinLength, ErrorMessage = ExcursionTitleMinLength)]
        [MaxLength(TitleMaxLength, ErrorMessage = ExcursionTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = ExcursionDestinationRequired)]
        [MinLength(DestinationMinLength, ErrorMessage = ExcursionDestinationMinLength)]
        [MaxLength(DestinationMaxLength, ErrorMessage = ExcursionDestinationMaxLength)]
        public string Destination { get; set; } = null!;

        [Required(ErrorMessage = ExcursionStartDateRequired)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = ExcursionEndDateRequired)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = ExcursionPriceRequired)]
        [Range((double)PriceMinValue, (double)PriceMaxValue, ErrorMessage = ExcursionPriceRange)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ExcursionCapacityRequired)]
        [Range(CapacityMinValue, CapacityMaxValue, ErrorMessage = ExcursionCapacityRange)]
        public int Capacity { get; set; }

        public string? ImageUrl { get; set; }
    }
}
