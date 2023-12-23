using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Countries
    {
        [Key]
        public int ID { get; set; }


        [Required]
        public string Name { get; set; }
    }
}
