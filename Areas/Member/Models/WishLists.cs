using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class WishLists
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey("User")]
        public string? UserID { get; set; }


        public virtual User? User { get; set; }


        public ICollection<WishListItems> WishListItems { get; set;}
    }
}
