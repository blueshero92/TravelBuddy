using System.ComponentModel.DataAnnotations;

namespace TravelBuddy.GCommon.CustomValidationAttributes
{
    // Validates that a DateTime value is at least N days after another property on the same model.
    public class MinDaysAfterPropertyAttribute : ValidationAttribute
    {
        private readonly string otherPropertyName;
        private readonly int days;

        public MinDaysAfterPropertyAttribute(string otherPropertyName, int days)
        {
            this.otherPropertyName = otherPropertyName;
            this.days = days;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(otherPropertyName);

            if (otherProperty == null)
            {
                return new ValidationResult($"Unknown property: {otherPropertyName}");
            }

            if (otherProperty.GetValue(validationContext.ObjectInstance) is not DateTime otherDate)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime date && date < otherDate.AddDays(days))
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Date must be at least {days} days after {otherPropertyName}.",
                    new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
