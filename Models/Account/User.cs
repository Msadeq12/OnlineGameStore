using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Areas.Member.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace PROG3050_HMJJ.Models.Account
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }


        public Profiles? Profiles { get; set; }


        public  Preferences? Preferences { get; set; }


        public Addresses? Addresses { get; set; }
    }
}
