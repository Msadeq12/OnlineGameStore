using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PROG3050_HMJJ.Services
{
    public class EventFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                if (dateToCheck >= DateTime.UtcNow)
                {
                    return ValidationResult.Success;
                }
            }
            else if (value is null)
            {
                return ValidationResult.Success;
            }

            string msg = ErrorMessage ?? $"{validationContext.DisplayName} must be a valid future or current date";
            return new ValidationResult(msg);
           
        }
    }
}
