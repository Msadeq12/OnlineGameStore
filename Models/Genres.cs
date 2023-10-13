using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Models
{
    public class Genres
    {
        [Key]
        public int ID { get; set; }


        public string Name { get; set; }
    }
}
