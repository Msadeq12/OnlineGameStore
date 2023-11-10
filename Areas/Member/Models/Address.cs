using PROG3050_HMJJ.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }

        public virtual User? User { get; set; }

        [Display(Name = "Address line 1")]
        public string? Line1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string? Line2 { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public int RegionID { get; set; }

        [Display(Name = "Delivery Instructions")]
        public string? DeliveryInstructions { get; set; }

        [Display(Name = "Mailing and Shipping Addresses Are the Same")]
        public Boolean SameAddress { get; set; }

        public string? AddressType { get; set; }
    }
}
