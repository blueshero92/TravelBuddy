using System.ComponentModel.DataAnnotations;

namespace TravelBuddy.GCommon.CustomValidationAttributes
{
    // Validates that a DateTime value is at least N days from today.
    public class MinDaysFromNowAttribute : ValidationAttribute
    {
        private readonly int days;

        public MinDaysFromNowAttribute(int days)
        {
            this.days = days;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date.Date < DateTime.UtcNow.Date.AddDays(days))
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Date must be at least {days} days from today.",
                    new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
