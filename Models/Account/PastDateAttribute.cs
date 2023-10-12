using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Models.Account
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext ctx)
        {
            if (value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;

                if (dateToCheck < DateTime.Today)
                {
                    return ValidationResult.Success;
                }
            }
            else if(value is null)
            {
                return ValidationResult.Success;
            }

            string msg = ErrorMessage ?? $"{ctx.DisplayName} must be a valid past date";
            return new ValidationResult(msg);
        }
    }
}
