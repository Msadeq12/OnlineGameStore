using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class SelectedPlatforms
    {
        [Key]
        public int ID { get; set; }


        public virtual Preferences Preferences { get; set; }


        public virtual Platforms Platforms { get; set; }
    }
}
