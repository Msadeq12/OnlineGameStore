using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Regions
    {
        [Key]
        public int ID { get; set; }


        [Required]
        public string Name { get; set; }


        [ForeignKey("Countries")]
        public int CountriesID { get; set; }


        public Countries Countries { get; set; }
    }
}
