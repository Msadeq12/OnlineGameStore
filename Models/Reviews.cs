using PROG3050_HMJJ.Areas.Admin.Models;
using PROG3050_HMJJ.Areas.Member.Models;
using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Models
{
    public class Reviews
    {
        [Key]
        public string commentId { get; set; }
        [Display(Name = "Add Your Comment")]
        public string commentText { get; set; }
        public string UserId { get; set; }
        [PastDate(ErrorMessage = "Date Added must be a valid date that's in the past.")]
        [Display(Name = "Date Added")]
        public DateTime Timestamp { get; set; }
        public GamesViewModel Games { get; set; }
    }
}
