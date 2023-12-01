using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;


namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class EventRegister
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public int eventID { get; set; } 
    }
}
