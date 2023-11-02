using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Address : IValidatableObject
    {
        [Key]
        public int ID { get; set; }

        public virtual User? user { get; set; }

        [Required(ErrorMessage = "Address line 1 is required")]
        [Display(Name = "Address line 1")]
        public string Line1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string? Line2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        public string PostalCode { get; set; }

        [Required]
        public virtual Countries Country { get; set; }

        [Required]
        public virtual Regions Region { get; set; }

        public string? DeliveryInstructions { get; set; }

        public Boolean SameAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!String.IsNullOrWhiteSpace(PostalCode))
            {
                if (Country != null)
                {
                    Regex regex;
                    if (Country.ID == 1)
                    {
                        regex = new Regex("^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] [0-9][ABCEGHJKMNPRSTVWXYZ][0-9]$");
                        if (!regex.Match(PostalCode).Success)
                        {
                            yield return new ValidationResult("A valid Postal Code is required", new List<string> { "PostalCode" });
                        }
                    }
                    else if (Country.ID == 2)
                    {
                        regex = new Regex("^\\d{5}(-{0,1}\\d{4})?$");
                        if (!regex.Match(PostalCode).Success)
                        {
                            yield return new ValidationResult("A valid Zip Code is required", new List<string> { "PostalCode" });
                        }
                    }
                }
                else
                {
                    yield return new ValidationResult("A country is required", new List<string> { "Country" });
                }
            }
            else
            {
                if(Region != null)
                {
                    if (Country != null)
                    {
                        string codeType = "";
                        if (Country.ID == 1)
                        {
                            codeType = "Postal Code";
                        }
                        else if (Country.ID == 2)
                        {
                            codeType = "Zip Code";
                        }
                        yield return new ValidationResult(String.Format("A {0} is required", codeType), new List<string> { "PostalCode" });
                    }
                }
            }
        }
    }
}
