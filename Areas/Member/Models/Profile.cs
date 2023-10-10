using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PROG3050_HMJJ.Models;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Profile
    {
        public int userID { get; set; }

        [Display(Name = "First Name")]
        public string? firstName { get; set; }

        [Display(Name = "Last Name")]
        public string? lastName { get; set; }

        [Display(Name = "Gender")]
        public string? gender { get; set; }

        [PastDate(ErrorMessage = "Birth date must be a valid date that's in the past.")]
        [Display(Name = "Birth Date")]
        public DateTime? DOB { get; set; }

        [Display(Name = "Recieve Promotional Emails")]
        public bool emails { get; set; }
    }
}
