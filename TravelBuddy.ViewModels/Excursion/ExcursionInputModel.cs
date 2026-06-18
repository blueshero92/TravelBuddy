using System.ComponentModel.DataAnnotations;
using TravelBuddy.GCommon.CustomValidationAttributes;
using static TravelBuddy.GCommon.Constants.OutputMessages;
using static TravelBuddy.GCommon.Constants.ValidationConstants.ExcursionConstants;

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
        [MinDaysFromNow(14, ErrorMessage = ExcursionStartDateTooSoon)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = ExcursionEndDateRequired)]
        [MinDaysAfterProperty(nameof(StartDate), 2, ErrorMessage = ExcursionEndDateTooSoon)]
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
