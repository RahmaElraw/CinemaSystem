using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Validations
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date <= DateTime.Now)
                {
                    return new ValidationResult("Date must be in the future");
                }
            }
            return ValidationResult.Success;
        }
    }
}