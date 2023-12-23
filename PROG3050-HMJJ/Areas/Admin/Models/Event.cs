using PROG3050_HMJJ.Areas.Member.Models;
using PROG3050_HMJJ.Services;
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
        [EventFutureDate(ErrorMessage = "Event Date cannot be in the past")]
        public DateTime StartDate { get; set; }
        [Required]
        [EventFutureDate(ErrorMessage = "Event Date cannot be in the past")]
        public DateTime EndDate { get; set; }

    }
}
