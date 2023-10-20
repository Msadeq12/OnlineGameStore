using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using PROG3050_HMJJ.Models.Account;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Profiles
    {
        [Key]
        public int ID { get; set; }


        public virtual User? User { get; set; }


        [Display(Name = "First Name")]
        public string? FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string? LastName { get; set; }


        [Display(Name = "Gender")]
        public string? Gender { get; set; }


        [PastDate(ErrorMessage = "Birth date must be a valid date that's in the past.")]
        [Display(Name = "Birth Date")]
        public DateTime? DOB { get; set; }


        [Display(Name = "Receive Promotional Emails")]
        public bool RecievePromotions { get; set; }
    }
}
