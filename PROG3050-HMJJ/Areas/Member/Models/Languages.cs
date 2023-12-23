using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Languages
    {
        [Key]
        public int ID { get; set; }


        public string Name { get; set; }
    }
}
