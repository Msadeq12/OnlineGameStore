using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.Account;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Preferences
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey("User")]
        public string UserID { get; set; }


        public virtual User User { get; set; }


        public virtual Languages? Languages { get; set; }
    }
}
