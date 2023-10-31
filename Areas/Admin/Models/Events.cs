using System.ComponentModel.DataAnnotations;

namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public sealed class Events
    {
        [Key]
        public int ID { get; set; }
        
        
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
