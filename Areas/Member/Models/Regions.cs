using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Regions
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int CountryID { get; set; }

        public Countries Country { get; set; }
    }
}
