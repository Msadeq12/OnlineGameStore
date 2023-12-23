using PROG3050_HMJJ.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Member.Models
{
    public class Reviews
    {
        [Key]
        public string CommentId { get; set; }
        [Required]
        public string CommentText { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public int GameId { get; set; } 
        public string GameName { get; set; }
        public bool? IsApproved { get; set; } = null;
    }
}
