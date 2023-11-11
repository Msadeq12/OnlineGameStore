using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class ShippingAddresses
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey("Addresses")]
        public int? AddressesID { get; set; }


        [ForeignKey("Regions")]
        public int? RegionsID { get; set; }


        public virtual Addresses Addresses { get; set; }


        public virtual Regions Regions { get; set; }


        [Display(Name = "Address line 1")]
        public string? Line1 { get; set; }


        [Display(Name = "Address Line 2")]
        public string? Line2 { get; set; }


        [Display(Name = "City")]
        public string? City { get; set; }


        public string? PostalCode { get; set; }
    }
}
