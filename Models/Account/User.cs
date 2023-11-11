using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Areas.Member.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace PROG3050_HMJJ.Models.Account
{
    public class User : IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }


        [ForeignKey("Profiles")]
        public int? ProfilesID { get; set; }


        [ForeignKey("Preferences")]
        public int? PreferencesID { get; set; }


        public virtual Profiles? Profiles { get; set; }


        public virtual Preferences? Preferences { get; set; }
    }
}
