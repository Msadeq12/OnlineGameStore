using PROG3050_HMJJ.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PROG3050_HMJJ.Areas.Admin.Models
{
    public class Games
    {
        [Key]
        public int ID { get; set; }


        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }


        [Required]
        public string Publisher { get; set; }


        [Required]
        public int ReleaseYear { get; set; }


        [Required]
        public decimal Price { get; set; }


        [ForeignKey("Genres")]
        public int GenresID { get; set; }


        [ForeignKey("Platforms")]
        public int PlatformsID { get; set; }


        public virtual Genres? Genres { get; set; }


        public virtual Platforms? Platforms { get; set; }


        //public Blob Icon { get; set; }
    }
}
