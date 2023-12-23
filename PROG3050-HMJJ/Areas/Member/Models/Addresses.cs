using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Addresses
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey("User")]
        public string? UserID { get; set; }


        public virtual User? User { get; set; }


        [Display(Name = "Delivery Instructions")]
        public string? DeliveryInstructions { get; set; }


        [Display(Name = "Mailing and Shipping Addresses Are the Same")]
        public bool SameAddress { get; set; }
    }
}
