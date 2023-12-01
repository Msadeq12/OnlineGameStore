using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class Event
    {
        public int eventID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
