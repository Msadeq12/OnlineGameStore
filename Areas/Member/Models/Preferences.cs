using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PROG3050_HMJJ.Models;
using PROG3050_HMJJ.Models.Account;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Preferences
    {
        [Key]
        public int ID { get; set; }


        public virtual User User { get; set; }


        public virtual Platforms? Platforms { get; set; }


        public virtual Genres? Genres { get; set; }


        public virtual Languages? Languages { get; set; }
    }
}
